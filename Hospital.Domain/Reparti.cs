using System.Collections.Generic;

namespace Hospital.Domain
{
    public class Reparti
    {
        public int Id { get; set; }
        public string EmriRepartit { get; set; }
        public string Lokacioni { get; set; }

        
        public ICollection<Mjeku> Mjeket { get; set; } = new List<Mjeku>();
    }
}