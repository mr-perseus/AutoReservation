using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.BusinessLayer
{
    public class ReservationManager
        : ManagerBase
    {
        public List<Reservation> List
        {
            get
            {
                using (AutoReservationContext context = new AutoReservationContext())
                {
                    

                    return context.Reservationen
                        .Include(r => r.Kunde)
                        .Include(r => r.Auto)
                        .ToList();
                }
            }
        }


        public void Add(Reservation reservation)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                try
                {
                    ValidateDateAndAuto(reservation);
                    context.Add(reservation);
                    context.SaveChanges();
                }
                catch (DbUpdateException exception)
                {
                    HandleDbUpdateException(exception, context, reservation);
                }
            }
        }

        public void Update(Reservation reservation)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                try
                {
                    ValidateDateAndAuto(reservation);
                    context.Update(reservation);
                    context.SaveChanges();
                }
                catch (DbUpdateException exception)
                {
                    HandleDbUpdateException(exception, context, reservation);
                }
            }
        }

        public void Remove(Reservation reservation)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                try
                {
                    context.Remove(reservation);
                    context.SaveChanges();
                }
                catch (DbUpdateException exception)
                {
                    HandleDbUpdateException(exception, context, reservation);
                }
            }
        }

        public Reservation GetById(int id)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                return context.Reservationen
                    .Include(r => r.Kunde)
                    .Include(r => r.Auto)
                    .FirstOrDefault(entry => entry.ReservationsNr == id);
            }
        }

        private static void ValidateDateAndAuto(Reservation reservation)
        {
            if (!AreDatesTwentyForHours(reservation.Von, reservation.Bis))
                throw new InvalidDateRangeException("Dates are invalid", reservation.Von, reservation.Bis);

            if (!IsAutoAvailable(reservation)) throw new AutoUnavailableException("Auto is not available");
        }

        private static bool AreDatesTwentyForHours(DateTime from, DateTime until)
        {
            return (until.Date - from.Date).TotalHours >= 24;
        }

        private static bool IsAutoAvailable(Reservation reservation)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                var count = (from Reservation in context.Reservationen
                    where Reservation.AutoId == reservation.AutoId &&
                          Reservation.ReservationsNr != reservation.ReservationsNr &&
                          ((reservation.Von <= Reservation.Von && reservation.Von > Reservation.Von) ||
                           (reservation.Von >= Reservation.Von && reservation.Von < Reservation.Bis))
                    select reservation);
                
                return !count.Any();
            }
        }
    }
}