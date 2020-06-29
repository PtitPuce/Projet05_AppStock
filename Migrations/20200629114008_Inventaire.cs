using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppStock.Migrations
{
    public partial class Inventaire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "app_nom_inventaire_statut",
                columns: table => new
                {
                    inventaire_statut_uid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    inventaire_statut_code = table.Column<string>(nullable: false),
                    inventaire_statut_libelle = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_nom_inventaire_statut", x => x.inventaire_statut_uid);
                });

            migrationBuilder.CreateTable(
                name: "app_inventaire",
                columns: table => new
                {
                    inventaire_uid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    inventaire_user_uid = table.Column<int>(nullable: false),
                    inventaire_statut_uid = table.Column<int>(nullable: false),
                    article_famille_uid = table.Column<int>(nullable: false),
                    inventaire_date_cloture = table.Column<DateTime>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false),
                    UserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_inventaire", x => x.inventaire_uid);
                    table.ForeignKey(
                        name: "FK_app_inventaire_app_article_famille_article_famille_uid",
                        column: x => x.article_famille_uid,
                        principalTable: "app_article_famille",
                        principalColumn: "article_famille_uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_app_inventaire_app_nom_inventaire_statut_inventaire_statut_u~",
                        column: x => x.inventaire_statut_uid,
                        principalTable: "app_nom_inventaire_statut",
                        principalColumn: "inventaire_statut_uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_app_inventaire_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "app_inventaire_ligne",
                columns: table => new
                {
                    inventaire_ligne_uid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    inventaire_ligne_inventaire_uid = table.Column<int>(nullable: false),
                    inventaire_ligne_article_uid = table.Column<int>(nullable: false),
                    inventaire_ligne_quantite_theorique = table.Column<int>(nullable: false),
                    inventaire_ligne_quantite_comptee = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_inventaire_ligne", x => x.inventaire_ligne_uid);
                    table.ForeignKey(
                        name: "FK_app_inventaire_ligne_app_article_inventaire_ligne_article_uid",
                        column: x => x.inventaire_ligne_article_uid,
                        principalTable: "app_article",
                        principalColumn: "article_uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_app_inventaire_ligne_app_inventaire_inventaire_ligne_inventa~",
                        column: x => x.inventaire_ligne_inventaire_uid,
                        principalTable: "app_inventaire",
                        principalColumn: "inventaire_uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_app_inventaire_article_famille_uid",
                table: "app_inventaire",
                column: "article_famille_uid");

            migrationBuilder.CreateIndex(
                name: "IX_app_inventaire_inventaire_statut_uid",
                table: "app_inventaire",
                column: "inventaire_statut_uid");

            migrationBuilder.CreateIndex(
                name: "IX_app_inventaire_UserId1",
                table: "app_inventaire",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_app_inventaire_ligne_inventaire_ligne_article_uid",
                table: "app_inventaire_ligne",
                column: "inventaire_ligne_article_uid");

            migrationBuilder.CreateIndex(
                name: "IX_app_inventaire_ligne_inventaire_ligne_inventaire_uid",
                table: "app_inventaire_ligne",
                column: "inventaire_ligne_inventaire_uid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_inventaire_ligne");

            migrationBuilder.DropTable(
                name: "app_inventaire");

            migrationBuilder.DropTable(
                name: "app_nom_inventaire_statut");
        }
    }
}
