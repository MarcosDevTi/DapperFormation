using DapperFormation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DapperFormation.EfPreparation.Maps
{
    public class PieceJointeMap : IEntityTypeConfiguration<PieceJointe>
    {
        public void Configure(EntityTypeBuilder<PieceJointe> builder)
        {
            builder.ToTable("oe_piece_jointe");
            builder.HasKey(p => p.Id).HasName("code");
            builder.Property(p => p.DocumentId).HasColumnName("id_document");
            builder.Property(p => p.AttestationId).HasColumnName("id_attestation");
            builder.Property(p => p.DeclarationId).HasColumnName("id_declaration");
        }
    }
}
