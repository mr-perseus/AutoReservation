using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using AutoReservation.BusinessLayer;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
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
                new AutoManager()
                    .Update(autoEntity);
            
        }

        public void DeleteAuto(AutoDto auto)
        {
            WriteActualMethod();
            Auto autoEntity = auto.ConvertToEntity();
            new AutoManager()
                .Remove(autoEntity);
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
            new KundeManager()
                .Update(kundeEntity);

        }

        public void DeleteKunde(KundeDto kunde)
        {
            WriteActualMethod();
            Kunde kundeEntity = kunde.ConvertToEntity();
            new KundeManager()
                .Remove(kundeEntity);
        }

        public List<KundeDto> KundeList()
        {
            WriteActualMethod();
            List<KundeDto> kundeDtos = new List<KundeDto>();
            List<Kunde> kundeEntities = new KundeManager().List;
            foreach (Kunde a in kundeEntities)
            {
                kundeDtos.Add(a.ConvertToDto());
            }
            return kundeDtos;
        }

        #endregion

        private static void WriteActualMethod()
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");
    }
}