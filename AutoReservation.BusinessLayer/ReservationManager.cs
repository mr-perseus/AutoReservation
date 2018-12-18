using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Reservation> ListWhere(Auto auto)
        {
            var list = List.Where(r => r.AutoId == auto.Id);
            return new List<Reservation>(list);
        }

        public void Add(Reservation reservation)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                try
                {
                    ValidateDateAndAuto(reservation);
                    context.Entry(reservation).State = EntityState.Added;
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
                    context.Entry(reservation).State = EntityState.Modified;
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
                    context.Entry(reservation).State = EntityState.Deleted;
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
            return DateTime.Compare(from.AddDays(1), until) <= 0;
        }

        private static bool IsAutoAvailable(Reservation reservationToCreate)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                var count = from reservation in context.Reservationen
                    where reservation.AutoId == reservationToCreate.AutoId &&
                          reservation.ReservationsNr != reservationToCreate.ReservationsNr &&
                          (reservationToCreate.Von <= reservation.Von && reservationToCreate.Von > reservation.Von ||
                           reservationToCreate.Von >= reservation.Von && reservationToCreate.Von < reservation.Bis)
                    select reservationToCreate;

                return !count.Any();
            }
        }
    }
}