using DapperFormation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DapperFormation.EfPreparation.Maps
{
    public class DeclarationMap : IEntityTypeConfiguration<Declaration>
    {
        public void Configure(EntityTypeBuilder<Declaration> builder)
        {
            builder.ToTable("oe_declaration");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.Nom).HasColumnName("nom");
            builder.Property(p => p.ProjetId).HasColumnName("id_projet");
        }
    }
}
