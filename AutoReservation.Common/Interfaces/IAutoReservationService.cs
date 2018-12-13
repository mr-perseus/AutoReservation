﻿using System.Collections.Generic;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Common.Interfaces
{

    [ServiceContract]
    public interface IAutoReservationService
    {
        [OperationContract]
        AutoDto GetAutoById(int autoId);

        [OperationContract]
        void InsertAuto(AutoDto auto);

        [OperationContract]
        void UpdateAuto(AutoDto auto);

        [OperationContract]
        void DeleteAuto(AutoDto auto);

        [OperationContract]
        List<AutoDto> AutoList();


        [OperationContract]
        KundeDto GetKundeById(int kundeId);

        [OperationContract]
        void InsertKunde(KundeDto auto);

        [OperationContract]
        void UpdateKunde(KundeDto auto);

        [OperationContract]
        void DeleteKunde(KundeDto auto);

        [OperationContract]
        List<KundeDto> KundeList();



    }
}
