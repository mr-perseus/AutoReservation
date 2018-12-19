using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects.Faults
{
    [DataContract]
    public class InvalidDateRangeFault
    {
        [DataMember] public string Operation { get; set; }

        [DataMember] public string Description { get; set; }
    }
}