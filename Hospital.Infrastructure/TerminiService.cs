using System;
using System.Linq;
using Hospital.Domain;

namespace Hospital.Infrastructure
{
    // 1. Ndërfaqja (Interface) që përcakton kontratën e shërbimit
    public interface ITerminiService
    {
        string CaktoTermin(int mjekId, int pacientId, DateTime data);
    }

    // 2. Implementimi i Shërbimit ku ndodhet logjika e biznesit
    public class TerminiService : ITerminiService
    {
        private readonly HospitalDbContext _context;

        // Injektimi i DbContext përmes Constructor-it
        public TerminiService(HospitalDbContext context)
        {
            _context = context;
        }

        public string CaktoTermin(int mjekId, int pacientId, DateTime data)
        {
            // Kontrollojmë nëse mjeku dhe pacienti ekzistojnë në databazë
            var mjeku = _context.Mjeket.Find(mjekId);
            var pacienti = _context.Pacientet.Find(pacientId);

            if (mjeku == null || pacienti == null)
            {
                return "Gabim: Mjeku ose Pacienti nuk ekziston!";
            }

            // RREGULLI I BIZNESIT (Gjenerata 22/23): 
            // Kontrollojmë nëse ky pacient ka tashmë një termin në të njëjtën ditë te ky mjek.
            // Përdorim .Date që të krahasojmë vetëm ditën/muajin/vitin (pa u ndikuar nga ora apo sekondat)
            var ekzistonTermini = _context.Terminet
                .Any(t => t.MjekuId == mjekId && t.PacientiId == pacientId && t.DataTerminit.Date == data.Date);

            if (ekzistonTermini)
            {
                return $"Gabim: Pacienti {pacienti.Emri} ka nje termin te caktuar me date {data.ToString("dd/MM/yyyy")} te ky mjek!";
            }

            // Nëse kontrolli kalon me sukses, krijojmë objektin e ri të Terminit
            var terminiiRi = new Termini
            {
                MjekuId = mjekId,
                PacientiId = pacientId,
                DataTerminit = data,
                Diagnoza = "Kontrolle e rregullt"
            };

            // Ruajtja fizike në SQL Server
            _context.Terminet.Add(terminiiRi);
            _context.SaveChanges();

            return "Termini u caktua me sukses!";
        }
    }
}