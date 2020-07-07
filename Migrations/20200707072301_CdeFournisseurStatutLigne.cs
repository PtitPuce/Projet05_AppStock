using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppStock.Migrations
{
    public partial class CdeFournisseurStatutLigne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_app_commande_app_nom_commande_type_commande_type_uid",
                table: "app_commande");

            migrationBuilder.DropTable(
                name: "app_nom_commande_type");

            migrationBuilder.DropIndex(
                name: "IX_app_commande_commande_type_uid",
                table: "app_commande");

            migrationBuilder.DropColumn(
                name: "commande_type_uid",
                table: "app_commande");

            migrationBuilder.CreateTable(
                name: "app_nom_commande_fournisseur_statut",
                columns: table => new
                {
                    commande_fournisseur_statut_uid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    commande_fournisseur_statut_code = table.Column<string>(nullable: false),
                    commande_fournisseur_statut_libelle = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_nom_commande_fournisseur_statut", x => x.commande_fournisseur_statut_uid);
                });

            migrationBuilder.CreateTable(
                name: "app_commande_fournisseur",
                columns: table => new
                {
                    commande_fournisseur_uid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    commande_fournisseur_numero = table.Column<string>(nullable: true),
                    commande_fournisseur_commentaire = table.Column<string>(nullable: true),
                    commande_fournisseur_contact_uid = table.Column<int>(nullable: false),
                    commande_fournisseur_fournisseur_uid = table.Column<int>(nullable: false),
                    commande_fournisseur_statut_uid = table.Column<int>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_commande_fournisseur", x => x.commande_fournisseur_uid);
                    table.ForeignKey(
                        name: "FK_app_commande_fournisseur_app_contact_commande_fournisseur_co~",
                        column: x => x.commande_fournisseur_contact_uid,
                        principalTable: "app_contact",
                        principalColumn: "contact_uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_app_commande_fournisseur_app_fournisseur_commande_fournisseu~",
                        column: x => x.commande_fournisseur_fournisseur_uid,
                        principalTable: "app_fournisseur",
                        principalColumn: "fournisseur_uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_app_commande_fournisseur_app_nom_commande_fournisseur_statut~",
                        column: x => x.commande_fournisseur_statut_uid,
                        principalTable: "app_nom_commande_fournisseur_statut",
                        principalColumn: "commande_fournisseur_statut_uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "app_commande_fournisseur_ligne",
                columns: table => new
                {
                    commande_fournisseur_ligne_uid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    commande_fournisseur_ligne_quantite = table.Column<int>(nullable: false),
                    commande_fournisseur_ligne_commande_fournisseur_uid = table.Column<int>(nullable: false),
                    commande_fournisseur_ligne_article_uid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_commande_fournisseur_ligne", x => x.commande_fournisseur_ligne_uid);
                    table.ForeignKey(
                        name: "FK_app_commande_fournisseur_ligne_app_article_commande_fourniss~",
                        column: x => x.commande_fournisseur_ligne_article_uid,
                        principalTable: "app_article",
                        principalColumn: "article_uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_app_commande_fournisseur_ligne_app_commande_fournisseur_comm~",
                        column: x => x.commande_fournisseur_ligne_commande_fournisseur_uid,
                        principalTable: "app_commande_fournisseur",
                        principalColumn: "commande_fournisseur_uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_app_commande_fournisseur_commande_fournisseur_contact_uid",
                table: "app_commande_fournisseur",
                column: "commande_fournisseur_contact_uid");

            migrationBuilder.CreateIndex(
                name: "IX_app_commande_fournisseur_commande_fournisseur_fournisseur_uid",
                table: "app_commande_fournisseur",
                column: "commande_fournisseur_fournisseur_uid");

            migrationBuilder.CreateIndex(
                name: "IX_app_commande_fournisseur_commande_fournisseur_statut_uid",
                table: "app_commande_fournisseur",
                column: "commande_fournisseur_statut_uid");

            migrationBuilder.CreateIndex(
                name: "IX_app_commande_fournisseur_ligne_commande_fournisseur_ligne_ar~",
                table: "app_commande_fournisseur_ligne",
                column: "commande_fournisseur_ligne_article_uid");

            migrationBuilder.CreateIndex(
                name: "IX_app_commande_fournisseur_ligne_commande_fournisseur_ligne_co~",
                table: "app_commande_fournisseur_ligne",
                column: "commande_fournisseur_ligne_commande_fournisseur_uid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_commande_fournisseur_ligne");

            migrationBuilder.DropTable(
                name: "app_commande_fournisseur");

            migrationBuilder.DropTable(
                name: "app_nom_commande_fournisseur_statut");

            migrationBuilder.AddColumn<int>(
                name: "commande_type_uid",
                table: "app_commande",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "app_nom_commande_type",
                columns: table => new
                {
                    commande_type_uid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    commande_type_code = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    commande_type_libelle = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_nom_commande_type", x => x.commande_type_uid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_app_commande_commande_type_uid",
                table: "app_commande",
                column: "commande_type_uid");

            migrationBuilder.AddForeignKey(
                name: "FK_app_commande_app_nom_commande_type_commande_type_uid",
                table: "app_commande",
                column: "commande_type_uid",
                principalTable: "app_nom_commande_type",
                principalColumn: "commande_type_uid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
