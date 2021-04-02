using Microsoft.EntityFrameworkCore.Migrations;

namespace DapperFormation.Migrations
{
    public partial class InitData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "oe_document",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nom_fichier = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oe_document", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "oe_professionnel",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nom = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oe_professionnel", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "oe_projet",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nom = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oe_projet", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "oe_attestation",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nom = table.Column<string>(type: "TEXT", nullable: true),
                    id_professionnel = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oe_attestation", x => x.id);
                    table.ForeignKey(
                        name: "FK_oe_attestation_oe_professionnel_id_professionnel",
                        column: x => x.id_professionnel,
                        principalTable: "oe_professionnel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "oe_declaration",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_projet = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oe_declaration", x => x.id);
                    table.ForeignKey(
                        name: "FK_oe_declaration_oe_projet_id_projet",
                        column: x => x.id_projet,
                        principalTable: "oe_projet",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "oe_piece_jointe",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titre = table.Column<string>(type: "TEXT", nullable: true),
                    id_document = table.Column<int>(type: "INTEGER", nullable: false),
                    id_attestation = table.Column<int>(type: "INTEGER", nullable: false),
                    id_declaration = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oe_piece_jointe", x => x.id);
                    table.ForeignKey(
                        name: "FK_oe_piece_jointe_oe_attestation_id_attestation",
                        column: x => x.id_attestation,
                        principalTable: "oe_attestation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_oe_piece_jointe_oe_declaration_id_declaration",
                        column: x => x.id_declaration,
                        principalTable: "oe_declaration",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_oe_piece_jointe_oe_document_id_document",
                        column: x => x.id_document,
                        principalTable: "oe_document",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_oe_attestation_id_professionnel",
                table: "oe_attestation",
                column: "id_professionnel");

            migrationBuilder.CreateIndex(
                name: "IX_oe_declaration_id_projet",
                table: "oe_declaration",
                column: "id_projet");

            migrationBuilder.CreateIndex(
                name: "IX_oe_piece_jointe_id_attestation",
                table: "oe_piece_jointe",
                column: "id_attestation");

            migrationBuilder.CreateIndex(
                name: "IX_oe_piece_jointe_id_declaration",
                table: "oe_piece_jointe",
                column: "id_declaration");

            migrationBuilder.CreateIndex(
                name: "IX_oe_piece_jointe_id_document",
                table: "oe_piece_jointe",
                column: "id_document");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "oe_piece_jointe");

            migrationBuilder.DropTable(
                name: "oe_attestation");

            migrationBuilder.DropTable(
                name: "oe_declaration");

            migrationBuilder.DropTable(
                name: "oe_document");

            migrationBuilder.DropTable(
                name: "oe_professionnel");

            migrationBuilder.DropTable(
                name: "oe_projet");
        }
    }
}
