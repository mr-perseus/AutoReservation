using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Common.DataTransferObjects.Faults
{
    [DataContract]
    public class OptimisticConcurrencyFault<T>
    {
        [DataMember]
        public T CurrentEntity { get; set; }

        [DataMember]
        public T FaultEntity { get; set; }
    }
}
