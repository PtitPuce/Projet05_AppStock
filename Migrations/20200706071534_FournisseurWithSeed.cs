using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppStock.Migrations
{
    public partial class FournisseurWithSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "app_fournisseur",
                columns: table => new
                {
                    fournisseur_uid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fournisseur_raison = table.Column<string>(nullable: true),
                    fournisseur_telephone = table.Column<string>(nullable: true),
                    fournisseur_email = table.Column<string>(nullable: true),
                    fournisseur_adresse_uid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_fournisseur", x => x.fournisseur_uid);
                    table.ForeignKey(
                        name: "FK_app_fournisseur_app_adresse_fournisseur_adresse_uid",
                        column: x => x.fournisseur_adresse_uid,
                        principalTable: "app_adresse",
                        principalColumn: "adresse_uid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_app_fournisseur_fournisseur_adresse_uid",
                table: "app_fournisseur",
                column: "fournisseur_adresse_uid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_fournisseur");
        }
    }
}
