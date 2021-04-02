using DapperFormation.EfPreparation.Maps;
using DapperFormation.Models;
using Microsoft.EntityFrameworkCore;

namespace DapperFormation.EfPreparation
{
    public class EfContext : DbContext
    {
        public EfContext(DbContextOptions<EfContext> options)
            : base(options) { }

        public DbSet<Projet> Projets { get; set; }
        public DbSet<Declaration> Declarations { get; set; }
        public DbSet<PieceJointe> PieceJointes { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Attestation> Attestations { get; set; }
        public DbSet<Professionnel> Professionnels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AttestationMap());
            modelBuilder.ApplyConfiguration(new DeclarationMap());
            modelBuilder.ApplyConfiguration(new DocumentMap());
            modelBuilder.ApplyConfiguration(new PieceJointeMap());
            modelBuilder.ApplyConfiguration(new ProfessionnelMap());
            modelBuilder.ApplyConfiguration(new ProjetMap());


            base.OnModelCreating(modelBuilder);
        }
    }
}
