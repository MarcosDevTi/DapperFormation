using Dapper;
using DapperFormation.EfPreparation;
using DapperFormation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        public ProjetsController(EfContext efContext, IConfiguration configuration)
        {
            _efContext = efContext;
            _configuration = configuration;

            var connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            _dapperConnection = connection;
        }

        public async Task<IActionResult> Index()
        {
            //var projets = _efContext.ProjetsIncludeAll().OrderBy(p => p.Nom);
            var projetsDapper = await ObtenirProjects();
            return View(projetsDapper);
        }

        public async Task<IEnumerable<Projet>> ObtenirProjects()
        {
            var sql = @"
            select 
            -- projet
            proj.id, proj.nom,
            -- déclaration
            decl.id, decl.nom, decl.id_projet ProjetId,
            -- pièce jointe
            pj.id, pj.titre, pj.id_document DocumentId, pj.id_attestation AttestationId,pj.id_declaration DeclarationId,
            -- Document
            doc.id, doc.nom_fichier NomFichier,
            -- Attestation
            attest.id, attest.nom, attest.id_professionnel ProfessionnelId,
            -- Professionnel
            prof.id, prof.nom
            from oe_projet proj
            inner join oe_declaration decl on decl.id_projet = proj.id
            inner join oe_piece_jointe pj on pj.id_declaration = decl.id
            inner join oe_document doc on doc.id = pj.id_document
            inner join oe_attestation attest on attest.id = pj.id_attestation
            inner join oe_professionnel prof on prof.id = attest.id_professionnel";

            var dictProjet = new Dictionary<int, Projet>();
            var dictDecl = new Dictionary<int, Declaration>();
            var dictPj = new Dictionary<int, PieceJointe>();

            var projets = await _dapperConnection.QueryAsync<Projet, Declaration, PieceJointe, Document, Attestation, Professionnel, Projet>(sql,
            (p, d, pj, doc, attest, prof) =>
            {
                if (!dictProjet.TryGetValue(p.Id, out Projet projet))
                    dictProjet.Add(p.Id, projet = p);

                var estDeclarationDejaAjoute = dictDecl.TryGetValue(d.Id, out Declaration declaration);
                if (!estDeclarationDejaAjoute)
                    dictDecl.Add(d.Id, declaration = d);

                if (!dictPj.TryGetValue(pj.Id, out PieceJointe pieceJointe))
                {
                     dictPj.Add(pj.Id, pieceJointe = pj);
                     pieceJointe.Document = doc;
                     attest.Professionnel = prof;
                     pieceJointe.Attestation = attest;
                }

                if (declaration.PiecesJointes is null)
                    declaration.PiecesJointes = new List<PieceJointe>();
                ((IList<PieceJointe>)declaration.PiecesJointes).Add(pieceJointe);

                if (projet.Declarations is null)
                    projet.Declarations = new List<Declaration>();

                if (!estDeclarationDejaAjoute)
                {
                    ((IList<Declaration>)projet.Declarations).Add(declaration);
                }

                return p;
            }
            );

            return dictProjet.Values;
        }
    }
}
