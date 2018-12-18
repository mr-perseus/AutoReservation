﻿using System;
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
            WriteActualMethod();
            Auto autoEntity = auto.ConvertToEntity();
            try
            {
                new AutoManager()
                    .Update(autoEntity);
            }
            catch (OptimisticConcurrencyException<Auto>)
            {
                OptimisticConcurrencyFault<AutoDto> fault = new OptimisticConcurrencyFault<AutoDto>
                {
                    FaultEntity = auto,
                    CurrentEntity = autoEntity.ConvertToDto()
                };
                throw new FaultException<OptimisticConcurrencyFault<AutoDto>>(fault);
            }

        }

        public void DeleteAuto(AutoDto auto)
        {
            WriteActualMethod();
            Auto autoEntity = auto.ConvertToEntity();
            try
            {

                new AutoManager()
                    .Remove(autoEntity);
            }
            catch (OptimisticConcurrencyException<Auto>)
            {
                OptimisticConcurrencyFault<AutoDto> fault = new OptimisticConcurrencyFault<AutoDto>
                {
                    FaultEntity = auto,
                    CurrentEntity = autoEntity.ConvertToDto()
                };
                throw new FaultException<OptimisticConcurrencyFault<AutoDto>>(fault);
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
            WriteActualMethod();
            Kunde kundeEntity = kunde.ConvertToEntity();
            try
            {

                new KundeManager()
                    .Update(kundeEntity);
            }
            catch (OptimisticConcurrencyException<KundeDto>)
            {
                OptimisticConcurrencyFault<KundeDto> fault = new OptimisticConcurrencyFault<KundeDto>
                {
                    FaultEntity = kunde,
                    CurrentEntity = kundeEntity.ConvertToDto()
                };
                throw new FaultException<OptimisticConcurrencyFault<KundeDto>>(fault);
            }
        }

        public void DeleteKunde(KundeDto kunde)
        {
            WriteActualMethod();
            Kunde kundeEntity = kunde.ConvertToEntity();
            try
            {

                new KundeManager()
                    .Remove(kundeEntity);
            }
            catch (OptimisticConcurrencyException<KundeDto>)
            {
                OptimisticConcurrencyFault<KundeDto> fault = new OptimisticConcurrencyFault<KundeDto>
                {
                    FaultEntity = kunde,
                    CurrentEntity = kundeEntity.ConvertToDto()
                };
                throw new FaultException<OptimisticConcurrencyFault<KundeDto>>(fault);
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
            catch (InvalidDateRangeException e)
            {
                throw new FaultException<InvalidDateRangeFault>(
                    new InvalidDateRangeFault
                    {
                        Operation = "Insert",
                        Description = "Reservation could not be inserted. Date invalid!"
                    }
                );

            }
            catch (AutoUnavailableException e)
            {
                throw new FaultException<AutoUnavailableFault>(
                    new AutoUnavailableFault
                    {
                        Operation = "Insert",
                        Description = "Reservation could not be inserted. Car already reserved!"

                    }
                    );
            }
             
        }

        public void UpdateReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            Reservation reservationEntity = reservation.ConvertToEntity();
            try
            {

                new ReservationManager()
                    .Update(reservationEntity);
            }
            catch (OptimisticConcurrencyException<ReservationDto>)
            {
                OptimisticConcurrencyFault<ReservationDto> fault = new OptimisticConcurrencyFault<ReservationDto>
                {
                    FaultEntity = reservation,
                    CurrentEntity = reservationEntity.ConvertToDto()
                };
                throw new FaultException<OptimisticConcurrencyFault<ReservationDto>>(fault);
            }
            catch (InvalidDateRangeException e)
            {
                throw new FaultException<InvalidDateRangeFault>(
                    new InvalidDateRangeFault
                    {
                        Operation = "Insert",
                        Description = "Reservation could not be inserted. Date invalid!"
                    }
                );

            }
            catch (AutoUnavailableException e)
            {
                throw new FaultException<AutoUnavailableFault>(
                    new AutoUnavailableFault
                    {
                        Operation = "Insert",
                        Description = "Reservation could not be inserted. Car already reserved!"

                    }
                );
            }
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            Reservation reservationEntity = reservation.ConvertToEntity();
            try
            {

                new ReservationManager()
                    .Remove(reservationEntity);
            }
            catch (OptimisticConcurrencyException<ReservationDto>)
            {
                OptimisticConcurrencyFault<ReservationDto> fault = new OptimisticConcurrencyFault<ReservationDto>
                {
                    FaultEntity = reservation,
                    CurrentEntity = reservationEntity.ConvertToDto()
                };
                throw new FaultException<OptimisticConcurrencyFault<ReservationDto>>(fault);
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
        

        public bool IsCarAvailable(AutoDto auto, DateTime date)
        {
            var list = new ReservationManager().ListWhere(auto.ConvertToEntity());
            foreach (var item in list)
            {
                if (item.Von > date || item.Bis < date)
                {
                    return false;
                }
            }
            return true;
        }





        #endregion

        private static void WriteActualMethod()
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");

    }
}