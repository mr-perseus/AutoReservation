using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects.Faults
{
    [DataContract]
    public class OptimisticConcurrencyFault
    {
        [DataMember] public string Operation { get; set; }

        [DataMember] public string Description { get; set; }
    }
}