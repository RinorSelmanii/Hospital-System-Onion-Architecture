using System;
using System.Collections.Generic;

namespace Hospital.Domain
{
    public class Pacienti
    {
        public int Id { get; set; }
        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public string NumriPersonal { get; set; }
        public DateTime DataLindjes { get; set; }

        
        public ICollection<Termini> Terminet { get; set; } = new List<Termini>();
    }
}