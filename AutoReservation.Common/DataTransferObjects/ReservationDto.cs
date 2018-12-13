using System;

namespace AutoReservation.Common.DataTransferObjects
{
    public class ReservationDto
    {
        public DateTime Bis { get; set; }

        public int ReservationsNr { get; set; }

        public byte[] RowVersion { get; set; }

        public DateTime Von { get; set; }

        public AutoDto Auto { get; set; }

        public KundeDto Kunde { get; set; }

        public override string ToString()
        {
            return $"{ReservationsNr}; {Von}; {Bis}; {Auto}; {Kunde}";
        }
    }
}