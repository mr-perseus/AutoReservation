using System;

namespace AutoReservation.BusinessLayer.Exceptions
{
    class AutoUnavailableException : Exception
    {
        public AutoUnavailableException(string message) : base(message) { }
    }
}
