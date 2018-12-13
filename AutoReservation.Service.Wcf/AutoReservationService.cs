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




        private static void WriteActualMethod()
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");
    }
}