using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitoring.Migrations
{
    public partial class QueryStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlobalQueryStats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    QueryHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExecCount = table.Column<long>(type: "bigint", nullable: true),
                    LastExecTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalWorkerTime = table.Column<long>(type: "bigint", nullable: true),
                    AvgCpuTime = table.Column<double>(type: "float", nullable: true),
                    TotalLogicalReads = table.Column<long>(type: "bigint", nullable: true),
                    TotalLogicalWrites = table.Column<long>(type: "bigint", nullable: true),
                    AvgIOsPerExecution = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalQueryStats", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalQueryStats");
        }
    }
}
