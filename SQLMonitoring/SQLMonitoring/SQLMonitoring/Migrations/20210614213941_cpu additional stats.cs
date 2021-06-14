using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitoring.Migrations
{
    public partial class cpuadditionalstats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlobalCPUStats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CPUTotalTime = table.Column<long>(type: "bigint", nullable: true),
                    CPUUserTime = table.Column<long>(type: "bigint", nullable: true),
                    CompileCPUTime = table.Column<long>(type: "bigint", nullable: true),
                    ExecCPUTime = table.Column<long>(type: "bigint", nullable: true),
                    WorkloadGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkloadGroupCPUTime = table.Column<long>(type: "bigint", nullable: true),
                    QueryHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QueryExecTime = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalCPUStats", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalCPUStats");
        }
    }
}
