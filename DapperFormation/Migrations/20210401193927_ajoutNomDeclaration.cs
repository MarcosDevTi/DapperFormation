using Microsoft.EntityFrameworkCore.Migrations;

namespace DapperFormation.Migrations
{
    public partial class ajoutNomDeclaration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "nom",
                table: "oe_declaration",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nom",
                table: "oe_declaration");
        }
    }
}
