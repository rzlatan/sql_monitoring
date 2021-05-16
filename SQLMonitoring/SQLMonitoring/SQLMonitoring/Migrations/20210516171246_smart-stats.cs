using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitoring.Migrations
{
    public partial class smartstats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SmartPredictionStats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Hour = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    ServerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CpuUsage = table.Column<double>(type: "float", nullable: false),
                    MemoryUsage = table.Column<double>(type: "float", nullable: false),
                    NetworkUsage = table.Column<double>(type: "float", nullable: false),
                    DiskLatency = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartPredictionStats", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmartPredictionStats");
        }
    }
}
