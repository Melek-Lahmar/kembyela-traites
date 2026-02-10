using Microsoft.EntityFrameworkCore;
using Kembyela.Models;

namespace Kembyela.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Traite> Traites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Traite>(entity =>
            {
                // =========================
                // Clé primaire
                // =========================
                entity.HasKey(e => e.Id);

                // =========================
                // Champs principaux
                // =========================
                entity.Property(e => e.Montant)
                    .HasPrecision(18, 3)
                    .IsRequired();

                entity.Property(e => e.RIB)
                    .HasMaxLength(20)
                    .IsRequired()
                    .IsFixedLength();

                entity.Property(e => e.Ville)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.DateEdition)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.DateEcheance)
                    .IsRequired();

                // =========================
                // Bénéficiaire & paiement
                // =========================
                entity.Property(e => e.OrdreDe)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(e => e.Payer)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(e => e.Aval)
                    .HasMaxLength(200);

                // =========================
                // Banque
                // =========================
                entity.Property(e => e.Banque)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(e => e.AdresseBanque)
                    .HasMaxLength(300);

                // =========================
                // Autres infos
                // =========================
                entity.Property(e => e.Monnaie)
                    .HasMaxLength(10)
                    .HasDefaultValue("DT");

                entity.Property(e => e.MontantEnLettres)
                    .HasMaxLength(500);

                entity.Property(e => e.Protestable)
                    .HasDefaultValue(true);

                entity.Property(e => e.EstPayee)
                    .HasDefaultValue(false);

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");

                // =========================
                // Index (performances)
                // =========================
                entity.HasIndex(e => e.RIB);
                entity.HasIndex(e => e.CreatedAt);
                entity.HasIndex(e => e.DateEcheance);
            });
        }
    }
}
