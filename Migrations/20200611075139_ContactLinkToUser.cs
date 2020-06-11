using Microsoft.EntityFrameworkCore.Migrations;

namespace AppStock.Migrations
{
    public partial class ContactLinkToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "contact_user_identity_uid",
                table: "app_contact",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_app_contact_contact_user_identity_uid",
                table: "app_contact",
                column: "contact_user_identity_uid");

            migrationBuilder.AddForeignKey(
                name: "FK_app_contact_AspNetUsers_contact_user_identity_uid",
                table: "app_contact",
                column: "contact_user_identity_uid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_app_contact_AspNetUsers_contact_user_identity_uid",
                table: "app_contact");

            migrationBuilder.DropIndex(
                name: "IX_app_contact_contact_user_identity_uid",
                table: "app_contact");

            migrationBuilder.DropColumn(
                name: "contact_user_identity_uid",
                table: "app_contact");
        }
    }
}
