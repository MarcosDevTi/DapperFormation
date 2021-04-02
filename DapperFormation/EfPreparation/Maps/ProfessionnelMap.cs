using DapperFormation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DapperFormation.EfPreparation.Maps
{
    public class ProfessionnelMap : IEntityTypeConfiguration<Professionnel>
    {
        public void Configure(EntityTypeBuilder<Professionnel> builder)
        {
            builder.ToTable("oe_professionnel");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.Nom).HasColumnName("nom");
        }
    }
}
