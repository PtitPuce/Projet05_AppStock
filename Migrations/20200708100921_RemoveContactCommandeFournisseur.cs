using Microsoft.EntityFrameworkCore.Migrations;

namespace AppStock.Migrations
{
    public partial class RemoveContactCommandeFournisseur : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_app_commande_fournisseur_app_contact_commande_fournisseur_co~",
                table: "app_commande_fournisseur");

            migrationBuilder.DropIndex(
                name: "IX_app_commande_fournisseur_commande_fournisseur_contact_uid",
                table: "app_commande_fournisseur");

            migrationBuilder.DropColumn(
                name: "commande_fournisseur_contact_uid",
                table: "app_commande_fournisseur");

            migrationBuilder.AddColumn<bool>(
                name: "commande_fournisseur_is_auto",
                table: "app_commande_fournisseur",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "commande_fournisseur_is_auto",
                table: "app_commande_fournisseur");

            migrationBuilder.AddColumn<int>(
                name: "commande_fournisseur_contact_uid",
                table: "app_commande_fournisseur",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_app_commande_fournisseur_commande_fournisseur_contact_uid",
                table: "app_commande_fournisseur",
                column: "commande_fournisseur_contact_uid");

            migrationBuilder.AddForeignKey(
                name: "FK_app_commande_fournisseur_app_contact_commande_fournisseur_co~",
                table: "app_commande_fournisseur",
                column: "commande_fournisseur_contact_uid",
                principalTable: "app_contact",
                principalColumn: "contact_uid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
