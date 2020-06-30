using Microsoft.EntityFrameworkCore.Migrations;

namespace AppStock.Migrations
{
    public partial class InventaireCorrectionUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_app_inventaire_AspNetUsers_UserId1",
                table: "app_inventaire");

            migrationBuilder.DropIndex(
                name: "IX_app_inventaire_UserId1",
                table: "app_inventaire");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "app_inventaire");

            migrationBuilder.AlterColumn<string>(
                name: "inventaire_user_uid",
                table: "app_inventaire",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_app_inventaire_inventaire_user_uid",
                table: "app_inventaire",
                column: "inventaire_user_uid");

            migrationBuilder.AddForeignKey(
                name: "FK_app_inventaire_AspNetUsers_inventaire_user_uid",
                table: "app_inventaire",
                column: "inventaire_user_uid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_app_inventaire_AspNetUsers_inventaire_user_uid",
                table: "app_inventaire");

            migrationBuilder.DropIndex(
                name: "IX_app_inventaire_inventaire_user_uid",
                table: "app_inventaire");

            migrationBuilder.AlterColumn<int>(
                name: "inventaire_user_uid",
                table: "app_inventaire",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "app_inventaire",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_app_inventaire_UserId1",
                table: "app_inventaire",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_app_inventaire_AspNetUsers_UserId1",
                table: "app_inventaire",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
