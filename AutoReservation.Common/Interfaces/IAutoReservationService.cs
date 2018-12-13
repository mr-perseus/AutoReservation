using System.Collections.Generic;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Common.Interfaces
{

    [ServiceContract]
    public interface IAutoReservationService
    {
        List<AutoDto> Autos { [OperationContract] get; }

        List<KundeDto> Kunde { [OperationContract] get; }

        List<ReservationDto> Reservation { [OperationContract] get; }


    }
}
