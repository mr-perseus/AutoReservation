using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    [Table("Reservation")]
    public class Reservation
    {
        public int AutoId { get; set; }

        [ForeignKey(nameof(AutoId))]
        public Auto Auto { get; set; }

        public DateTime Bis { get; set; }

        public int KundeId { get; set; }

        [ForeignKey(nameof(KundeId))]
        public Kunde Kunde { get; set; }

        public int ReservationsNr { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public DateTime Von { get; set; }
    }
}