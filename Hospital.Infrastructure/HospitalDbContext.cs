using Microsoft.EntityFrameworkCore;
using Hospital.Domain;
using System;

namespace Hospital.Infrastructure
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext() { }

        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        // 1. Këtu regjistrohen klasat e Domain-it si tabela në SQL Server
        public DbSet<Mjeku> Mjeket { get; set; }
        public DbSet<Pacienti> Pacientet { get; set; }
        public DbSet<Termini> Terminet { get; set; }
        public DbSet<Reparti> Repartet { get; set; }

       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=SpitaliDb;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        // 3. Këtu konfigurijmë lidhjen Many-to-Many përmes Fluent API (Pyetja 5 për pikë maksimale)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Termini>().HasKey(t => t.Id);

            
            modelBuilder.Entity<Termini>()
                .HasOne(t => t.Mjeku)
                .WithMany(m => m.Terminet)
                .HasForeignKey(t => t.MjekuId);

            
            modelBuilder.Entity<Termini>()
                .HasOne(t => t.Pacienti)
                .WithMany(p => p.Terminet)
                .HasForeignKey(t => t.PacientiId);
        }
    }
}