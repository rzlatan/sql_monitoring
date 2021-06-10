using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitoring.Migrations
{
    public partial class moreandmorebasicinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "DatabaseFile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsEncrypted",
                table: "Database",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecoveryModel",
                table: "Database",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SnapshotIsolationLevel",
                table: "Database",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Database",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "DatabaseFile");

            migrationBuilder.DropColumn(
                name: "IsEncrypted",
                table: "Database");

            migrationBuilder.DropColumn(
                name: "RecoveryModel",
                table: "Database");

            migrationBuilder.DropColumn(
                name: "SnapshotIsolationLevel",
                table: "Database");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Database");
        }
    }
}
