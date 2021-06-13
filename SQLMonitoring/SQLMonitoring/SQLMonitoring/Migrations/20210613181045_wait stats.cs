using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitoring.Migrations
{
    public partial class waitstats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlobalSpinlockStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Server = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Spinlock = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Collisions = table.Column<long>(type: "bigint", nullable: false),
                    Backoffs = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalSpinlockStats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GlobalWaitStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WaitType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WaitTimeMs = table.Column<long>(type: "bigint", nullable: false),
                    MaxWaitTimeMs = table.Column<long>(type: "bigint", nullable: false),
                    SignalWaitTimeMs = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalWaitStats", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalSpinlockStats");

            migrationBuilder.DropTable(
                name: "GlobalWaitStats");
        }
    }
}
