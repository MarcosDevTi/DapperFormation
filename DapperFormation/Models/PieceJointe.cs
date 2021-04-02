namespace DapperFormation.Models
{
    public class PieceJointe
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public int DocumentId { get; set; }
        public Document Document { get; set; }
        public int AttestationId { get; set; }
        public Attestation Attestation { get; set; }
        public int DeclarationId { get; set; }

    }
}
