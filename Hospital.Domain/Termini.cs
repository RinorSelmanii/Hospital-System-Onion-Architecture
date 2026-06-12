using System;

namespace Hospital.Domain
{
    public class Termini
    {
        public int Id { get; set; }
        public DateTime DataTerminit { get; set; }
        public string Diagnoza { get; set; }

       
        public int MjekuId { get; set; }
        public Mjeku Mjeku { get; set; }

      
        public int PacientiId { get; set; }
        public Pacienti Pacienti { get; set; }
    }
}