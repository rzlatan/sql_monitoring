using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitoring.Migrations
{
    public partial class MemoryStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlobalMemoryStats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ServerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalMemory = table.Column<long>(type: "bigint", nullable: true),
                    TargetMemory = table.Column<long>(type: "bigint", nullable: true),
                    BufferCacheHitRatio = table.Column<long>(type: "bigint", nullable: true),
                    PageLifeExpectancy = table.Column<long>(type: "bigint", nullable: true),
                    MemoryClerk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemoryClerkSize = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalMemoryStats", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalMemoryStats");
        }
    }
}
