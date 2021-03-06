using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    [Table("Kunde")]
    public class Kunde
    {
        public DateTime Geburtsdatum { get; set; }

        [Key] public int Id { get; set; }

        public string Nachname { get; set; }

        [Timestamp] public byte[] RowVersion { get; set; }

        public string Vorname { get; set; }

        public ICollection<Reservation> Reservationen { get; set; }
    }
}