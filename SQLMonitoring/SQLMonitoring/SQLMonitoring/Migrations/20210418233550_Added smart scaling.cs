using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitoring.Migrations
{
    public partial class Addedsmartscaling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SmartPredictionsEnabled",
                table: "Servers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SmartPredictionsEnabled",
                table: "Servers");
        }
    }
}
