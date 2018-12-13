using System;

namespace AutoReservation.Common.DataTransferObjects
{
    public class KundeDto
    {
        public DateTime Geburtsdatum { get; set; }

        public int Id { get; set; }

        public string Nachname { get; set; }

        public byte[] RowVersion { get; set; }

        public string Vorname { get; set; }

        public override string ToString()
        {
            return $"{Id}; {Nachname}; {Vorname}; {Geburtsdatum}; {RowVersion}";
        }
    }
}