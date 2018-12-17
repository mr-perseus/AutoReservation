using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using AutoReservation.BusinessLayer;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Dal.Entities;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {
        #region Auto
        public AutoDto GetAutoById(int autoId)
        {
            WriteActualMethod();
            return new AutoManager()
                .GetById(autoId)
                .ConvertToDto();
        }

        public void InsertAuto(AutoDto auto)
        {
            WriteActualMethod();
            Auto autoEntity = auto.ConvertToEntity();
            new AutoManager()
                .Add(autoEntity);
        }

        public void UpdateAuto(AutoDto auto)
        {

            try
            {
                WriteActualMethod();
                Auto autoEntity = auto.ConvertToEntity();
                new AutoManager()
                    .Update(autoEntity);
            }
            catch (OptimisticConcurrencyException<Auto> e)
            {
                throw new FaultException<OptimisticConcurrencyFault>(
                    new OptimisticConcurrencyFault
                    {
                        Operation = "Update",
                        Description = "Auto could not be updated. Optimistic Concurrency Fault!"
                    }
                    );
            }

        }

        public void DeleteAuto(AutoDto auto)
        {
            try
            {
                WriteActualMethod();
                Auto autoEntity = auto.ConvertToEntity();
                new AutoManager()
                    .Remove(autoEntity);
            }
            catch (OptimisticConcurrencyException<Auto> e)
            {
                throw new FaultException<OptimisticConcurrencyFault>(
                    new OptimisticConcurrencyFault
                    {
                        Operation = "Delete",
                        Description = "Auto could not be removed. Either it was edited by someone else or it has been removed already."
                    }
                        
                );
            }
        }

        public List<AutoDto> AutoList()
        {
            WriteActualMethod();
            List<AutoDto> autoDtos = new List<AutoDto>();
            List<Auto> autoEntities = new AutoManager().List;
            foreach (Auto a in autoEntities)
            {
                autoDtos.Add(a.ConvertToDto());
            }
            return autoDtos;
        }

        #endregion

        #region Kunde

        public KundeDto GetKundeById(int kundeId)
        {
            WriteActualMethod();
            return new KundeManager()
                .GetById(kundeId)
                .ConvertToDto();
        }

        public void InsertKunde(KundeDto kunde)
        {
            WriteActualMethod();
            Kunde kundeEntity = kunde.ConvertToEntity();
            new KundeManager()
                .Add(kundeEntity);
        }

        public void UpdateKunde(KundeDto kunde)
        {
            try
            {
                WriteActualMethod();
                Kunde kundeEntity = kunde.ConvertToEntity();
                new KundeManager()
                    .Update(kundeEntity);
            }
            catch (OptimisticConcurrencyException<Kunde> e)
            {
                throw new FaultException<OptimisticConcurrencyFault>(
                    new OptimisticConcurrencyFault
                    {
                        Operation = "Update",
                        Description = "Kunde could not be updated. OptimisticConcurrency Fault!"
                    }
                );
            }
        }

        public void DeleteKunde(KundeDto kunde)
        {
            try
            {
                WriteActualMethod();
                Kunde kundeEntity = kunde.ConvertToEntity();
                new KundeManager()
                    .Remove(kundeEntity);
            }
            catch (OptimisticConcurrencyException<Kunde> e)
            {
                throw new FaultException<OptimisticConcurrencyFault>(
                    new OptimisticConcurrencyFault
                    {
                        Operation = "Delete",
                        Description = "Kunde could not be removed. Either it was edited by someone else or it has been removed already."
                    }
                );
            }
        }

        public List<KundeDto> KundeList()
        {
            WriteActualMethod();
            List<KundeDto> kundeDtos = new List<KundeDto>();
            List<Kunde> kundeEntities = new KundeManager().List;
            foreach (Kunde k in kundeEntities)
            {
                kundeDtos.Add(k.ConvertToDto());
            }
            return kundeDtos;
        }

        #endregion

        #region Reservation

        public ReservationDto GetReservationById(int reservationId)
        {
            WriteActualMethod();
            return new ReservationManager()
                .GetById(reservationId)
                .ConvertToDto();
        }

        public void InsertReservation(ReservationDto reservation)
        {
            try
            {
                WriteActualMethod();
                Reservation reservationEntity = reservation.ConvertToEntity();
                new ReservationManager()
                    .Add(reservationEntity);
            }
            catch(OptimisticConcurrencyException<Reservation>)
            {

            }
             
        }

        public void UpdateReservation(ReservationDto reservation)
        {
            try
            {
                WriteActualMethod();
                Reservation reservationEntity = reservation.ConvertToEntity();
                new ReservationManager()
                    .Update(reservationEntity);
            }
            catch (OptimisticConcurrencyException<Reservation> e)
            {
                throw new FaultException<OptimisticConcurrencyFault>(
                    new OptimisticConcurrencyFault
                    {
                        Operation = "Update",
                        Description = "Reservation could not be updated. OptimisticConcurrency Fault!"
                    }
                );
            }
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            try
            {
                WriteActualMethod();
                Reservation reservationEntity = reservation.ConvertToEntity();
                new ReservationManager()
                    .Remove(reservationEntity);
            }
            catch (OptimisticConcurrencyException<Reservation> e)
            {
                throw new FaultException<OptimisticConcurrencyFault>(
                    new OptimisticConcurrencyFault
                    {
                        Operation = "Delete",
                        Description = "Reservation could not be removed. Either it was edited by someone else or it has been removed already."
                    }
                );
            }
        }

        public List<ReservationDto> ReservationList()
        {
            List<ReservationDto> reservationDtos = new List<ReservationDto>();
            List<Reservation> reservationEntities = new ReservationManager().List;
            foreach (Reservation r in reservationEntities)
            {
                reservationDtos.Add(r.ConvertToDto());
            }

            return reservationDtos;
        }

        #endregion

        private static void WriteActualMethod()
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");
    }
}