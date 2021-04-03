using DapperFormation.Models;
using System.Collections.Generic;

namespace DapperFormation.EfPreparation.Seed
{
    public static class InitData
    {
        public static void SeedInit(this EfContext ef)
        {
            var listProjets = new List<Projet>
            {
                new Projet
                {
                    Nom = "Projet 1",
                    Declarations = new List<Declaration>
                    {
                        new Declaration
                        {
                            Nom = "Déclaration 1 - Projet 1",
                            PiecesJointes = new List<PieceJointe>
                            {
                                MonterPieceJointe(
                                    titre: "Pièce jointe 1 - Déclaration 1",
                                    document: "Document 1 - Pièce jointe 1"),
                                MonterPieceJointe(
                                    titre: "Pièce jointe 2 - Déclaration 1",
                                    attestation: "Attestation 1 - Pièce jointe 2",
                                    professionnel: "Professionnel 1 - Attestation 1"),
                                MonterPieceJointe(
                                    titre:"Pièce jointe 3 - Déclaration 1",
                                    document: "Document 2 - Pièce jointe 3",
                                    attestation: "Attestation 2 - Pièce jointe 3",
                                    professionnel: "Professionnel 2 - Attestation 2"
                                    )
                            }
                        },
                        new Declaration
                        {
                            Nom = "Déclaration 2 - Projet 1",
                            PiecesJointes = new List<PieceJointe>
                            {
                                MonterPieceJointe(
                                    titre: "Pièce jointe 4 - Déclaration 2",
                                    document: "Document 3 - Pièce jointe 4"),
                                MonterPieceJointe(
                                    titre:"Pièce jointe 5 - Déclaration 2",
                                    document: "Document 4 - Pièce jointe 5",
                                    attestation: "Attestation 3 - Pièce jointe 5",
                                    professionnel: "Professionnel 3 - Attestation 3"
                                    )
                            }
                        },
                        new Declaration
                        {
                            Nom = "Déclaration 3 - projet 1",
                        }
                    }
                },
                new Projet
                {
                    Nom = "Projet 2",
                    Declarations = new List<Declaration>
                    {
                        new Declaration
                        {
                            Nom = "Déclaration 4 - projet 2",
                            PiecesJointes = new List<PieceJointe>
                            {
                                MonterPieceJointe(
                                    titre: "Pièce jointe 6 - Déclaration 4",
                                    document: "Document 5 - Pièce jointe 6"),
                                MonterPieceJointe(
                                    titre: "Pièce jointe 7 - Déclaration 4",
                                    document: "Document 6 - Pièce jointe 7"),
                            }
                        },
                        new Declaration
                        {
                            Nom = "Déclaration 5 - projet 2",
                            PiecesJointes = new List<PieceJointe>
                            {
                                 MonterPieceJointe(
                                    titre:"Pièce jointe 8 - Déclaration 5",
                                    document: "Document 7 - Pièce jointe 8",
                                    attestation: "Attestation 4 - Pièce jointe 8",
                                    professionnel: "Professionnel 4 - Attestation 4"
                                    ),
                                 MonterPieceJointe(
                                    titre: "Pièce jointe 9 - Déclaration 5",
                                    document: "Document 8 - Pièce jointe 9"),
                            }
                        }
                    }
                },
                new Projet
                {
                    Nom = "Projet 3",
                },
            };

            ef.Projets.AddRange(listProjets);
            ef.SaveChanges();
        }

        public static PieceJointe MonterPieceJointe(
            string titre, string document = null, string attestation = null, string professionnel = null)
        {
            var pieceJointe = new PieceJointe { Titre = titre };

            if (document is not null)
                pieceJointe.Document = new Document { NomFichier = document };

            if (attestation is not null)
                pieceJointe.Attestation = new Attestation { Nom = attestation };

            if (professionnel is not null)
                pieceJointe.Attestation.Professionnel = new Professionnel { Nom = professionnel };

            return pieceJointe;
        }
    }
}
