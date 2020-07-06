using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppStock.Migrations
{
    public partial class CommandesFournisseurLigneStatut : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "CommandeFournisseurEntities",
                columns: table => new
                {
                    commande_fournisseur_uid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    commande_fournisseur_numero = table.Column<string>(nullable: true),
                    commande_fournisseur_commentaire = table.Column<string>(nullable: true),
                    commande_fournisseur_contact_uid = table.Column<int>(nullable: false),
                    commande_fournisseur_fournisseur_uid = table.Column<int>(nullable: false),
                    commande_fournisseur_statut_uid = table.Column<int>(nullable: false),
                    commande_fournisseur_type_uid = table.Column<int>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandeFournisseurEntities", x => x.commande_fournisseur_uid);
                    table.ForeignKey(
                        name: "FK_CommandeFournisseurEntities_app_contact_commande_fournisseur~",
                        column: x => x.commande_fournisseur_contact_uid,
                        principalTable: "app_contact",
                        principalColumn: "contact_uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandeFournisseurEntities_app_fournisseur_commande_fournis~",
                        column: x => x.commande_fournisseur_fournisseur_uid,
                        principalTable: "app_fournisseur",
                        principalColumn: "fournisseur_uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandeFournisseurEntities_app_nom_commande_fournisseur_sta~",
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
                        name: "FK_app_commande_fournisseur_ligne_CommandeFournisseurEntities_c~",
                        column: x => x.commande_fournisseur_ligne_commande_fournisseur_uid,
                        principalTable: "CommandeFournisseurEntities",
                        principalColumn: "commande_fournisseur_uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_app_commande_fournisseur_ligne_commande_fournisseur_ligne_ar~",
                table: "app_commande_fournisseur_ligne",
                column: "commande_fournisseur_ligne_article_uid");

            migrationBuilder.CreateIndex(
                name: "IX_app_commande_fournisseur_ligne_commande_fournisseur_ligne_co~",
                table: "app_commande_fournisseur_ligne",
                column: "commande_fournisseur_ligne_commande_fournisseur_uid");

            migrationBuilder.CreateIndex(
                name: "IX_CommandeFournisseurEntities_commande_fournisseur_contact_uid",
                table: "CommandeFournisseurEntities",
                column: "commande_fournisseur_contact_uid");

            migrationBuilder.CreateIndex(
                name: "IX_CommandeFournisseurEntities_commande_fournisseur_fournisseur~",
                table: "CommandeFournisseurEntities",
                column: "commande_fournisseur_fournisseur_uid");

            migrationBuilder.CreateIndex(
                name: "IX_CommandeFournisseurEntities_commande_fournisseur_statut_uid",
                table: "CommandeFournisseurEntities",
                column: "commande_fournisseur_statut_uid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_commande_fournisseur_ligne");

            migrationBuilder.DropTable(
                name: "CommandeFournisseurEntities");

            migrationBuilder.DropTable(
                name: "app_nom_commande_fournisseur_statut");
        }
    }
}
