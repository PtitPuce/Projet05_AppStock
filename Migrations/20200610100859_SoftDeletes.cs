using Microsoft.EntityFrameworkCore.Migrations;

namespace AppStock.Migrations
{
    public partial class SoftDeletes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "app_stock",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "app_nom_type_tva",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "app_article_famille",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "app_article",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "app_stock");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "app_nom_type_tva");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "app_article_famille");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "app_article");
        }
    }
}
