using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitoring.Migrations
{
    public partial class ServerOwnber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servers_Users_UserId",
                table: "Servers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Servers",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Servers_UserId",
                table: "Servers",
                newName: "IX_Servers_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servers_Users_OwnerId",
                table: "Servers",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servers_Users_OwnerId",
                table: "Servers");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Servers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Servers_OwnerId",
                table: "Servers",
                newName: "IX_Servers_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servers_Users_UserId",
                table: "Servers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
