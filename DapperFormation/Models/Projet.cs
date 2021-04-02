using System.Collections.Generic;

namespace DapperFormation.Models
{
    public class Projet
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public IEnumerable<Declaration> Declarations { get; set; }
    }
}
