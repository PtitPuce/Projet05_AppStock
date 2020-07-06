using Microsoft.EntityFrameworkCore.Migrations;

namespace AppStock.Migrations
{
    public partial class SuppressionTypeCommande : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_app_commande_app_nom_commande_type_commande_type_uid",
                table: "app_commande");

            migrationBuilder.DropIndex(
                name: "IX_app_commande_commande_type_uid",
                table: "app_commande");

            migrationBuilder.DropColumn(
                name: "commande_fournisseur_type_uid",
                table: "CommandeFournisseurEntities");

            migrationBuilder.DropColumn(
                name: "commande_type_uid",
                table: "app_commande");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "commande_fournisseur_type_uid",
                table: "CommandeFournisseurEntities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "commande_type_uid",
                table: "app_commande",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
