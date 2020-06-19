using Microsoft.EntityFrameworkCore.Migrations;

namespace AppStock.Migrations
{
    public partial class CommandeAdresse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "commande_adresse_uid",
                table: "app_commande",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_app_commande_commande_adresse_uid",
                table: "app_commande",
                column: "commande_adresse_uid");

            migrationBuilder.AddForeignKey(
                name: "FK_app_commande_app_adresse_commande_adresse_uid",
                table: "app_commande",
                column: "commande_adresse_uid",
                principalTable: "app_adresse",
                principalColumn: "adresse_uid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_app_commande_app_adresse_commande_adresse_uid",
                table: "app_commande");

            migrationBuilder.DropIndex(
                name: "IX_app_commande_commande_adresse_uid",
                table: "app_commande");

            migrationBuilder.DropColumn(
                name: "commande_adresse_uid",
                table: "app_commande");
        }
    }
}
