using DapperFormation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DapperFormation.EfPreparation.Maps
{
    public class DocumentMap : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("oe_document");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.NomFichier).HasColumnName("nom_fichier");
        }
    }
}
