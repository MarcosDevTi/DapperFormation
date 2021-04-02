using Dapper;
using DapperFormation.EfPreparation;
using DapperFormation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DapperFormation.Controllers
{
    public class ProjetsController : Controller
    {
        private readonly EfContext _efContext;
        private readonly IDbConnection connection;

        public ProjetsController(EfContext efContext)
        {
            _efContext = efContext;

            var conn = new SqliteConnection($"Data Source=FormationDb.sqlite");
            conn.Open();
            connection = conn;
        }

        public async Task<IActionResult> Index()
        {
            var projetsDapper = await ObtenirProjets();

            var projets = _efContext.ProjetsIncludeAll().OrderBy(p => p.Nom);
            return View(projetsDapper);
        }


        public async Task<IEnumerable<Projet>> ObtenirProjets()
        {
            var sql = @"select 
                        -- projet
                        proj.id, proj.nom,
                        -- déclaration
                        decl.id, decl.nom, decl.id_projet ProjetId,
                        -- pièce jointe
                        pj.id, pj.titre, pj.id_document DocumentId, pj.id_attestation AttestationId, pj.id_declaration DeclarationId,
                        -- document
                        doc.id, doc.nom_fichier NomFichier,
                        -- attestation
                        attest.id, attest.nom, attest.id_professionnel ProfessionnelId,
                        -- professionnel
                        prof.id, prof.nom
                        from oe_projet proj
                        inner join oe_declaration decl on decl.id_projet = proj.id
                        inner join oe_piece_jointe pj on pj.id_declaration = decl.id
                        inner join oe_document doc on doc.id = pj.id_document
                        inner join oe_attestation attest on attest.id = pj.id_attestation
                        inner join oe_professionnel prof on prof.id = attest.id_professionnel
                        order by proj.nom";

            var projDect = new Dictionary<int, Projet>();
            var declDict = new Dictionary<int, Declaration>();
            var blequerDeclaration = false;
            var pjDict = new Dictionary<int, PieceJointe>();

            var resultat = await connection.QueryAsync<Projet, Declaration, PieceJointe, Document, Attestation, Professionnel, Projet>(sql,
                  (p, d, pj, doc, attest, prof) =>
                  {
                      if (!projDect.TryGetValue(p.Id, out Projet projet))
                          projDect.Add(p.Id, projet = p);

                      if (!declDict.TryGetValue(d.Id, out Declaration declaration))
                      {
                          declDict.Add(d.Id, declaration = d);
                          blequerDeclaration = true;
                      }

                      if (!pjDict.TryGetValue(pj.Id, out PieceJointe pieceJointe))
                      {
                          pjDict.Add(pj.Id, pieceJointe = pj);
                          pieceJointe.Document = doc;
                          attest.Professionnel = prof;
                          pieceJointe.Attestation = attest;
                      }

                      if (declaration.PiecesJointes is null)
                          declaration.PiecesJointes = new List<PieceJointe>();
                      ((List<PieceJointe>)declaration.PiecesJointes).Add(pieceJointe);

                      if (projet.Declarations is null)
                          projet.Declarations = new List<Declaration>();
                      if (blequerDeclaration)
                      {
                          ((List<Declaration>)projet.Declarations).Add(declaration);
                          blequerDeclaration = false;
                      }

                      return projet;
                  }
            );

            var res = projDect.Values;
            return res;
        }
    }
}
