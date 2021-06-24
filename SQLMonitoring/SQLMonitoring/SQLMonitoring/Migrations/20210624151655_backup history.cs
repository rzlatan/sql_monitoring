using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitoring.Migrations
{
    public partial class backuphistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlobalBackupStats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    DatabaseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackupStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BackupEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BackupDuration = table.Column<long>(type: "bigint", nullable: true),
                    BackupType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackupSize = table.Column<long>(type: "bigint", nullable: true),
                    BackupLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastRestorablePoint = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HoursSinceLastBackup = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalBackupStats", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalBackupStats");
        }
    }
}
