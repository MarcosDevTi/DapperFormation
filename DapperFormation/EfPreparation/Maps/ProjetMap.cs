using DapperFormation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DapperFormation.EfPreparation.Maps
{
    public class ProjetMap : IEntityTypeConfiguration<Projet>
    {
        public void Configure(EntityTypeBuilder<Projet> builder)
        {
            builder.ToTable("oe_projet");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.Nom).HasColumnName("nom");
        }
    }
}
