using System.Collections.Generic;
using System.Linq;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.BusinessLayer
{
    public class AutoManager
        : ManagerBase
    {
        public List<Auto> List
        {
            get
            {
                using (AutoReservationContext context = new AutoReservationContext())
                {
                    return context.Autos.ToList();
                }
            }
        }

        public void Add(Auto auto)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                try
                {
                    context.Add(auto);
                    context.SaveChanges();
                }
                catch (DbUpdateException exception)
                {
                    HandleDbUpdateException(exception, context, auto);
                }
            }
        }

        public void Update(Auto auto)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                try
                {
                    context.Update(auto);
                    context.SaveChanges();
                }
                catch (DbUpdateException exception)
                {
                    HandleDbUpdateException(exception, context, auto);
                }
            }
        }

        public void Remove(Auto auto)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                try
                {
                    context.Remove(auto);
                    context.SaveChanges();
                }
                catch (DbUpdateException exception)
                {
                    HandleDbUpdateException(exception, context, auto);
                }
            }
        }

        public Auto GetById(int id)
        {
            return List.FirstOrDefault(entry => entry.Id == id);
        }
    }
}