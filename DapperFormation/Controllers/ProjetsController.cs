using DapperFormation.EfPreparation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DapperFormation.Controllers
{
    public class ProjetsController : Controller
    {
        private readonly EfContext _efContext;

        public ProjetsController(EfContext efContext) => _efContext = efContext;

        public async Task<IActionResult> Index()
        {
            var projets = _efContext.Projets
                .IncludePieceJointe().ThenInclude(pj => pj.Attestation).ThenInclude(a => a.Professionnel)
                .IncludePieceJointe().ThenInclude(d => d.Document).OrderBy(p => p.Nom);

            return View(projets);
        }
    }
}
