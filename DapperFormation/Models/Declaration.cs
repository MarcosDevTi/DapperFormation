using System.Collections.Generic;

namespace DapperFormation.Models
{
    public class Declaration
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int? ProjetId { get; set; }
        public ICollection<PieceJointe> PiecesJointes { get; set; } = new List<PieceJointe>();
    }
}
