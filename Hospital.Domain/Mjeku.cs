using System.Collections.Generic;

namespace Hospital.Domain
{
    public class Mjeku
    {
        public int Id { get; set; }
        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public string Specializimi { get; set; }

        
        public int RepartiId { get; set; }
        public Reparti Repartit { get; set; }

        public ICollection<Termini> Terminet { get; set; } = new List<Termini>();
    }
}