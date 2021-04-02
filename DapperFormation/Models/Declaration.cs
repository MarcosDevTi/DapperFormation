using System.Collections.Generic;

namespace DapperFormation.Models
{
    public class Declaration
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int ProjetId { get; set; }
        public IEnumerable<PieceJointe> PiecesJointes { get; set; }
    }
}
