using DapperFormation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Linq;

namespace DapperFormation.EfPreparation
{
    public static class EfExtensions
    {
        public static IQueryable<Projet> ProjetsIncludeAll(this DbSet<Projet> dbSet) =>
             dbSet.IncludePieceJointe().ThenInclude(pj => pj.Attestation).ThenInclude(a => a.Professionnel)
                .IncludePieceJointe().ThenInclude(d => d.Document);

        public static IIncludableQueryable<Projet, IEnumerable<PieceJointe>> IncludePieceJointe(this DbSet<Projet> dbSet) =>
             dbSet.Include(p => p.Declarations).ThenInclude(d => d.PiecesJointes);

        public static IIncludableQueryable<Projet, IEnumerable<PieceJointe>> IncludePieceJointe(
            this IIncludableQueryable<Projet, Professionnel> includable) =>
             includable.Include(p => p.Declarations).ThenInclude(d => d.PiecesJointes);
    }
}
