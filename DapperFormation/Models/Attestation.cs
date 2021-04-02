namespace DapperFormation.Models
{
    public class Attestation
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int ProfessionnelId { get; set; }
        public Professionnel Professionnel { get; set; }
    }
}
