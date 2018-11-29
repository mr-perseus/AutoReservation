using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    [Table("Auto")]
    public abstract class Auto
    {
        public int Id { get; set; }

        public string Marke { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public int Tagestarif { get; set; }

        public ICollection<Reservation> Reservationen { get; set; }
    }
}
