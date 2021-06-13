using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitoring.Migrations
{
    public partial class modifieddbtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IndexSize",
                table: "Database");

            migrationBuilder.DropColumn(
                name: "ReservedSize",
                table: "Database");

            migrationBuilder.DropColumn(
                name: "UnusedSize",
                table: "Database");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IndexSize",
                table: "Database",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReservedSize",
                table: "Database",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnusedSize",
                table: "Database",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
