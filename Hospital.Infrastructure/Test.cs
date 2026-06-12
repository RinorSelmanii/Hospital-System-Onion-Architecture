using System;
using Hospital.Domain;

namespace Hospital.Infrastructure
{
    public class Test
    {
        // Pika e nisjes për ekzekutim
        public static void Main(string[] args)
        {
            Console.WriteLine("=== DUKE NISUR TESTIMIN E SISTEMIT SPITALOR ===");

            using (var context = new HospitalDbContext())
            {
                // 1. Pastrojmë databazën që testet të nisin të pastra çdo herë
                context.Terminet.RemoveRange(context.Terminet);
                context.Mjeket.RemoveRange(context.Mjeket);
                context.Pacientet.RemoveRange(context.Pacientet);
                context.Repartet.RemoveRange(context.Repartet);
                context.SaveChanges();

                Console.WriteLine("1. Databaza u pastrua.");

                // 2. Krijojmë një Repart fillestar
                var repartiKardiologjisë = new Reparti { EmriRepartit = "Kardiologji", Lokacioni = "Kati 2" };
                context.Repartet.Add(repartiKardiologjisë);
                var repartiFizioterapis = new Reparti { EmriRepartit = "Fizioterapi", Lokacioni = "Kati 1" };
                context.Repartet.Add(repartiFizioterapis);
                context.SaveChanges();

                // 3. Krijojmë një Mjek në atë repart
                var mjeku = new Mjeku
                {
                    Emri = "Agon",
                    Mbiemri = "Krasniqi",
                    Specializimi = "Kardiolog",
                    RepartiId = repartiKardiologjisë.Id
                };
                context.Mjeket.Add(mjeku);
                var mjeku2 = new Mjeku
                {
                    Emri = "Teuta",
                    Mbiemri = "Krasniqi",
                    Specializimi = "Fizioterapiste",
                    RepartiId = repartiFizioterapis.Id
                };
              
                context.Mjeket.Add(mjeku2);

                // 4. Krijojmë një Pacient
                var pacienti = new Pacienti
                {
                    Emri = "Besnik",
                    Mbiemri = "Gashi",
                    NumriPersonal = "1234567890",
                    DataLindjes = new DateTime(1995, 5, 15)
                };
                context.Pacientet.Add(pacienti);
                context.SaveChanges();

                Console.WriteLine("2. Mjeku dhe Pacienti u regjistruan në SQL Server.");
                Console.WriteLine("--------------------------------------------------");

                // 5. Thërrasim Shërbimin e Termineve për të testuar rregullin
                var terminiService = new TerminiService(context);
                DateTime dataEAnjëjtë = new DateTime(2026, 6, 20, 10, 0, 0); // 20 Qershor 2026, ora 10:00
                DateTime dataE = new DateTime(2026, 6, 21, 10, 0, 0); // 21 Qershor 2026, ora 10:00

                // TESTI 1: Caktimi i terminit të parë (Duhet të ketë sukses!)
                Console.WriteLine("TESTI 1: Caktimi i terminit të parë...");
                string rezultati1 = terminiService.CaktoTermin(mjeku.Id, pacienti.Id, dataEAnjëjtë);
                string rezultati2 = terminiService.CaktoTermin(mjeku2.Id, pacienti.Id, dataE);
                Console.WriteLine($"Rezultati: {rezultati1}");
                Console.WriteLine($"Rezultati: {rezultati2}");


                Console.WriteLine();

                // TESTI 2: Tentimi për të caktuar termin të dytë në po atë datë te i njëjti mjek (Duhet të bllokohet!)
                Console.WriteLine("TESTI 2: Tentimi për të caktuar termin të dytë në të njëjtën datë...");
                DateTime oraTjetërPorDataEAnjëjtë = new DateTime(2026, 6, 20, 14, 0, 0); // Ora 14:00, por po e njëjta ditë
                DateTime oraTjeter = new DateTime(2026, 6, 21, 14, 0, 0);
                string rezultati3 = terminiService.CaktoTermin(mjeku.Id, pacienti.Id, oraTjetërPorDataEAnjëjtë);
                string rezultati4 = terminiService.CaktoTermin(mjeku2.Id, pacienti.Id, oraTjeter);
                Console.WriteLine($"Rezultati: {rezultati3}");
                Console.WriteLine($"Rezultati: {rezultati4}");
            }

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Shtyp një buton për të mbyllur konsolën...");
            Console.ReadKey();
        }
    }
}