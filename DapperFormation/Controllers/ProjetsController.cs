using Dapper;
using DapperFormation.EfPreparation;
using DapperFormation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DapperFormation.Controllers
{
    public class ProjetsController : Controller
    {
        private readonly EfContext _efContext;
        private readonly IDbConnection _dapperConnection;

        public ProjetsController(EfContext efContext, IDbConnection dapperConnection)
        {
            _efContext = efContext;
            _dapperConnection = dapperConnection;
        }

        public async Task<IActionResult> EfListProjets()
        {
            var projets = await _efContext.Projets
                .IncludePieceJointe().ThenInclude(pj => pj.Attestation).ThenInclude(a => a.Professionnel)
                .IncludePieceJointe().ThenInclude(d => d.Document).OrderBy(p => p.Nom).ToListAsync();

            return View("Index", projets);
        }

        public async Task<IActionResult> DapperListProjets()
        {
            var sql = @"select 
                        -- projet
                        proj.id, proj.nom,
                        -- déclaration
                        decl.id, decl.nom, decl.id_projet ProjetId,
                        -- pièce jointe
                        pj.code, pj.titre, pj.id_document DocumentId, 
                        pj.id_attestation AttestationId, pj.id_declaration DeclarationId,
                        -- document
                        doc.id, doc.nom_fichier NomFichier,
                        -- attestation
                        attest.id, attest.nom, attest.id_professionnel ProfessionnelId,
                        prof.id, prof.nom
                        from oe_projet proj
                        left join oe_declaration decl on decl.id_projet = proj.id
                        left join oe_piece_jointe pj on pj.id_declaration = decl.id
                        left join oe_document doc on doc.id = pj.id_document
                        left join oe_attestation attest on attest.id = pj.id_attestation
                        left join oe_professionnel prof on prof.id = attest.id_professionnel";

            var dictProj = new Dictionary<int, Projet>();
            var dictDecl = new Dictionary<int, Declaration>();

            await _dapperConnection.QueryAsync<
                Projet, Declaration, PieceJointe, Document, Attestation, Professionnel, Projet>(sql,
                (proj, decl, pj, doc, attest, prof) =>
                {
                    var estProjetDéjàAjouté = dictProj.TryGetValue(proj.Id, out Projet projet);
                    if (!estProjetDéjàAjouté)
                        dictProj.Add(proj.Id, projet = proj);

                    if (decl is not null)
                    {
                        var estDéclarationDéjàAjouté = dictDecl.TryGetValue(decl.Id, out Declaration declaration);
                        if (!estDéclarationDéjàAjouté)
                            dictDecl.Add(decl.Id, declaration = decl);

                        if (pj is not null)
                        {
                            pj.Document = doc;
                            if (attest is not null)
                            {
                                attest.Professionnel = prof;
                            }
                            pj.Attestation = attest;
                            declaration.PiecesJointes.Add(pj);
                        }

                        if (!estDéclarationDéjàAjouté)
                            projet.Declarations.Add(decl);
                    }

                    return proj;
                }, splitOn: "id, id, code, id, id, id");
            return View("Index", dictProj.Values);
        }
    }
}
