using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppStock.Migrations
{
    public partial class ContactEtAdresseStart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "app_adresse",
                columns: table => new
                {
                    adresse_uid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    adresse_champ_1 = table.Column<string>(nullable: true),
                    adresse_champ_2 = table.Column<string>(nullable: true),
                    adresse_code_postal = table.Column<string>(nullable: true),
                    adresse_ville = table.Column<string>(nullable: true),
                    adresse_pays = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_adresse", x => x.adresse_uid);
                });

            migrationBuilder.CreateTable(
                name: "app_contact",
                columns: table => new
                {
                    contact_uid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    contact_nom = table.Column<string>(nullable: true),
                    contact_prenom = table.Column<string>(nullable: true),
                    contact_telephone = table.Column<string>(nullable: true),
                    contact_adresse_uid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_contact", x => x.contact_uid);
                    table.ForeignKey(
                        name: "FK_app_contact_app_adresse_contact_adresse_uid",
                        column: x => x.contact_adresse_uid,
                        principalTable: "app_adresse",
                        principalColumn: "adresse_uid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_app_contact_contact_adresse_uid",
                table: "app_contact",
                column: "contact_adresse_uid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_contact");

            migrationBuilder.DropTable(
                name: "app_adresse");
        }
    }
}
