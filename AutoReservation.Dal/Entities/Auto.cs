using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    [Table("Auto")]
    public class Auto
    {
        public int Id { get; set; }

        public string Marke { get; set; }

        public byte[] RowVersion { get; set; }

        public int Tagestarif { get; set; }

        public ICollection<Reservation> Reservationen { get; set; }
    }
}
