using DapperFormation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DapperFormation.EfPreparation.Maps
{
    public class AttestationMap : IEntityTypeConfiguration<Attestation>
    {
        public void Configure(EntityTypeBuilder<Attestation> builder)
        {
            builder.ToTable("oe_attestation");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.Nom).HasColumnName("nom");
            builder.Property(p => p.ProfessionnelId).HasColumnName("id_professionnel");
        }
    }
}
