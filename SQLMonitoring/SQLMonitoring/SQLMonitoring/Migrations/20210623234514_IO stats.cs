using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitoring.Migrations
{
    public partial class IOstats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlobalIOStats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalMBps = table.Column<long>(type: "bigint", nullable: true),
                    WriteMBps = table.Column<long>(type: "bigint", nullable: true),
                    ReadMBps = table.Column<long>(type: "bigint", nullable: true),
                    TotalIOPS = table.Column<long>(type: "bigint", nullable: true),
                    ReadIOPS = table.Column<long>(type: "bigint", nullable: true),
                    WriteIOPS = table.Column<long>(type: "bigint", nullable: true),
                    ReadLatency = table.Column<long>(type: "bigint", nullable: true),
                    WriteLatency = table.Column<long>(type: "bigint", nullable: true),
                    PlanId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalIOs = table.Column<long>(type: "bigint", nullable: true),
                    TotalReadIOs = table.Column<long>(type: "bigint", nullable: true),
                    TotalWriteIOs = table.Column<long>(type: "bigint", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalIOStats", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalIOStats");
        }
    }
}
