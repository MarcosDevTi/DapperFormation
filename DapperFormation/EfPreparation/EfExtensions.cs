using DapperFormation.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DapperFormation.EfPreparation
{
    public static class EfExtensions
    {
        public static IEnumerable<Projet> ProjetsIncludeAll(this EfContext context)
        {
            return context.Projets
                .Include(p => p.Declarations).ThenInclude(d => d.PiecesJointes).ThenInclude(pj => pj.Attestation).ThenInclude(a => a.Professionnel)
                .Include(a => a.Declarations).ThenInclude(d => d.PiecesJointes).ThenInclude(d => d.Document);
        }
    }
}
