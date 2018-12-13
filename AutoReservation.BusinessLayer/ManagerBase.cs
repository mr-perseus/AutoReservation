using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.BusinessLayer
{
    public abstract class ManagerBase
    {
        protected void HandleDbUpdateException<T>(DbUpdateException exception, AutoReservationContext context, T entity)
            where T : class
        {
            if (exception is DbUpdateConcurrencyException) throw CreateOptimisticConcurrencyException(context, entity);

            // TODO Other possibilities
        }

        protected static OptimisticConcurrencyException<T> CreateOptimisticConcurrencyException<T>(
            AutoReservationContext context, T entity)
            where T : class
        {
            T dbEntity = (T) context.Entry(entity)
                .GetDatabaseValues()
                .ToObject();

            return new OptimisticConcurrencyException<T>($"Update {typeof(T).Name}: Concurrency-Fehler", dbEntity);
        }
    }
}