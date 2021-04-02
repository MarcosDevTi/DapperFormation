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
                            Nom = "Déclaration 01 - projet 1",
                            PiecesJointes = new List<PieceJointe>
                            {
                                BuildPieceJointe( projet: 1, pieceJointe: 1, document: 1),
                                BuildPieceJointe( projet: 1, pieceJointe: 2, document: 2),
                                BuildPieceJointe( projet: 1, pieceJointe: 3, document: 3)
                            }
                        },
                        new Declaration
                        {
                            Nom = "Déclaration 02 - projet 1",
                            PiecesJointes = new List<PieceJointe>
                            {
                                BuildPieceJointe( projet: 1, pieceJointe: 4, document: 4),
                                BuildPieceJointe( projet: 1, pieceJointe: 5, document: 5),
                            }
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
                            Nom = "Déclaration 03 - projet 2",
                            PiecesJointes = new List<PieceJointe>
                            {
                                BuildPieceJointe( projet: 2, pieceJointe: 6, document: 6),
                                BuildPieceJointe( projet: 2, pieceJointe: 7, document: 7),
                            }
                        },
                        new Declaration
                        {
                            Nom = "Déclaration 04 - projet 2",
                            PiecesJointes = new List<PieceJointe>
                            {
                                BuildPieceJointe( projet: 2, pieceJointe: 8, document: 8),
                                BuildPieceJointe( projet: 2, pieceJointe: 9, document: 9),
                                BuildPieceJointe( projet: 2, pieceJointe: 10, document: 10)
                            }
                        },
                        new Declaration
                        {
                            Nom = "Déclaration 05 - projet 2",
                            PiecesJointes = new List<PieceJointe>
                            {
                                BuildPieceJointe( projet: 2, pieceJointe: 11, document: 11),
                            }
                        }
                    }
                },

            };

            ef.AddRange(listProjets);
            ef.SaveChanges();
        }

        public static PieceJointe BuildPieceJointe(int projet, int pieceJointe, int document)
        {
            return CreerPieceJointe(pieceJointe, projet, CreerDocument(document, pieceJointe, projet), CreerAttestation(document, pieceJointe, projet, CreerProfessionnel(document, document, pieceJointe, projet)));
        }

        private static PieceJointe CreerPieceJointe(int pieceJointe, int projet, Document document, Attestation attestation)
        {
            return new PieceJointe
            {
                Titre = $"Piece Jointe {pieceJointe} - projet {projet}",
                Document = document,
                Attestation = attestation
            };
        }

        private static Document CreerDocument(int document, int pieceJointe, int projet)
        {
            return new Document
            {
                NomFichier = $"Document {document} - PieceJointe {pieceJointe} - Projet {projet}"
            };
        }

        private static Attestation CreerAttestation(int attestation, int pieceJointe, int projet, Professionnel professionnel)
        {
            return new Attestation
            {
                Nom = $"Attestation {attestation} - PieceJointe {pieceJointe} - Projet {projet}",
                Professionnel = professionnel
            };
        }


        private static Professionnel CreerProfessionnel(int professionel, int attestation, int pieceJointe, int projet)
        {
            return new Professionnel
            {
                Nom = $"Professionnel {professionel} - Attestation {attestation} - PieceJointe {pieceJointe} - Projet {projet}"
            };
        }
    }
}
