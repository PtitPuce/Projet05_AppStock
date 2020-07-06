using Microsoft.EntityFrameworkCore.Migrations;

namespace AppStock.Migrations
{
    public partial class ArticleFournisseurAndSeuilSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "article_fournisseur_uid",
                table: "app_article",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "article_threshold",
                table: "app_article",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_app_article_article_fournisseur_uid",
                table: "app_article",
                column: "article_fournisseur_uid");

            migrationBuilder.AddForeignKey(
                name: "FK_app_article_app_fournisseur_article_fournisseur_uid",
                table: "app_article",
                column: "article_fournisseur_uid",
                principalTable: "app_fournisseur",
                principalColumn: "fournisseur_uid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_app_article_app_fournisseur_article_fournisseur_uid",
                table: "app_article");

            migrationBuilder.DropIndex(
                name: "IX_app_article_article_fournisseur_uid",
                table: "app_article");

            migrationBuilder.DropColumn(
                name: "article_fournisseur_uid",
                table: "app_article");

            migrationBuilder.DropColumn(
                name: "article_threshold",
                table: "app_article");
        }
    }
}
