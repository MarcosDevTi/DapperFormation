using Dapper;
using DapperFormation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DapperFormation.Controllers
{
    public class DapperController : Controller
    {
        private readonly IDbConnection _db;
        private readonly IConfiguration _config;

        public DapperController(IConfiguration config)
        {
            _config = config;
            var conn = new SqliteConnection(_config.GetConnectionString("DefaultConnection"));
            conn.Open();

            _db = conn;
        }

        public async Task<IActionResult> Index()
        {
            var sql = @"
            select 
            -- projet
            proj.id, proj.nom,
            -- déclaration
            decl.id, decl.nom, decl.id_projet ProjetId,
            -- pièce jointe
            pj.code, pj.titre, pj.id_document DocumentId, pj.id_attestation AttestationId, pj.id_declaration DeclarationId,
             -- document
            doc.id, doc.nom_fichier NomFichier,
             -- Attestation
            attest.id, attest.nom, attest.id_professionnel ProfessionnelId,
            -- Professionnel
            prof.id, prof.nom
             from oe_projet proj
             left join oe_declaration decl on decl.id_projet = proj.id
             left join oe_piece_jointe pj on pj.id_declaration = decl.id
             left join oe_document doc on doc.id = pj.id_document
             left join oe_attestation attest on attest.id = pj.id_attestation
             left join oe_professionnel prof on prof.id = attest.id_professionnel";

            var dictProjet = new Dictionary<int, Projet>();
            var dicDecl = new Dictionary<int, Declaration>();
            var pjDict = new Dictionary<int, PieceJointe>();
            await _db.QueryAsync<Projet, Declaration, PieceJointe, Document, Attestation, Professionnel, Projet>(sql,
                (proj, decl, pj, doc, attest, prof) =>
                {
                    var estProjetDéjàSélectionné = dictProjet.TryGetValue(proj.Id, out Projet projet);
                    if (!estProjetDéjàSélectionné)
                        dictProjet.Add(proj.Id, projet = proj);

                    if (projet.Declarations is null)
                        projet.Declarations = new List<Declaration>();

                    if (decl is not null)
                    {
                        var estDéclarationDéjàSélectionné = dicDecl.TryGetValue(decl.Id, out Declaration declaration);
                        if (!estDéclarationDéjàSélectionné)
                            dicDecl.Add(decl.Id, declaration = decl);

                        if (declaration.PiecesJointes is null)
                            declaration.PiecesJointes = new List<PieceJointe>();

                        if (pj is not null)
                        {
                            if (!pjDict.TryGetValue(pj.Code, out PieceJointe pieceJointe))
                            {
                                pjDict.Add(pj.Code, pieceJointe = pj);
                            }

                            pieceJointe.Document = doc;

                            if (attest is not null)
                            {
                                attest.Professionnel = prof;
                                pieceJointe.Attestation = attest;
                            }

                            ((IList<PieceJointe>)declaration.PiecesJointes).Add(pieceJointe);
                        }

                        if (!estDéclarationDéjàSélectionné)
                            ((IList<Declaration>)projet.Declarations).Add(declaration);
                    }

                    return proj;
                },  /*👉*/ splitOn: "id, code, id, id, id");

            return View(dictProjet.Values);
        }
    }
}