﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Common.DataTransferObjects.Faults
{
    [DataContract]
    class AutoUnavailableFault
    {
        [DataMember]
        public string Operation { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
