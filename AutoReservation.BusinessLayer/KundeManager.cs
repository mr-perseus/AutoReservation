using System.Collections.Generic;
using System.Linq;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.BusinessLayer
{
    public class KundeManager
        : ManagerBase
    {
        public List<Kunde> List
        {
            get
            {
                using (AutoReservationContext context = new AutoReservationContext())
                {
                    return context.Kunden.ToList();
                }
            }
        }

        public void Add(Kunde kunde)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                try
                {
                    context.Add(kunde);
                    context.SaveChanges();
                }
                catch (DbUpdateException exception)
                {
                    HandleDbUpdateException(exception, context, kunde);
                }
            }
        }

        public void Update(Kunde kunde)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                try
                {
                    context.Update(kunde);
                    context.SaveChanges();
                }
                catch (DbUpdateException exception)
                {
                    HandleDbUpdateException(exception, context, kunde);
                }
            }
        }

        public void Remove(Kunde kunde)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                try
                {
                    context.Remove(kunde);
                    context.SaveChanges();
                }
                catch (DbUpdateException exception)
                {
                    HandleDbUpdateException(exception, context, kunde);
                }
            }
        }
    }
}