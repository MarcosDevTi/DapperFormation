using System.Collections.Generic;

namespace DapperFormation.Models
{
    public class Projet
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public ICollection<Declaration> Declarations { get; set; } = new List<Declaration>();
    }
}
