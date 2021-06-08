using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitoring.Migrations
{
    public partial class resoureusagestats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BasicResourceUsageStats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CpuUsage = table.Column<double>(type: "float", nullable: false),
                    MemoryUsage = table.Column<double>(type: "float", nullable: false),
                    NetworkUsage = table.Column<double>(type: "float", nullable: false),
                    BatchRequests = table.Column<double>(type: "float", nullable: false),
                    UserConnections = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicResourceUsageStats", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasicResourceUsageStats");
        }
    }
}
