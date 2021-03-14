using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitoring.Migrations
{
    public partial class ExtenderServerSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Servers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Servers");
        }
    }
}
