using System;

namespace AutoReservation.BusinessLayer.Exceptions
{
    public class InvalidDateRangeException : Exception
    {
        public DateTime Von { get; set; }
        public DateTime Bis { get; set; }

        public InvalidDateRangeException(string message) : base(message) { }

        public InvalidDateRangeException(string message, DateTime from, DateTime until) : base(message)
        {
            Von = from;
            Bis = until;
        }
    }
}
