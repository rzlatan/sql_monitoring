using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitoring.Migrations
{
    public partial class CommonProblems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlobalCommonProblems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    DbName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentSizeMb = table.Column<double>(type: "float", nullable: true),
                    Growth = table.Column<int>(type: "int", nullable: true),
                    FreeSpaceMb = table.Column<double>(type: "float", nullable: true),
                    TransactionId = table.Column<int>(type: "int", nullable: true),
                    TransactionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeginTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Duration = table.Column<long>(type: "bigint", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoursSinceLastBackup = table.Column<long>(type: "bigint", nullable: true),
                    DatabaseId = table.Column<long>(type: "bigint", nullable: true),
                    EqualityColumns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InequalityColumns = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalCommonProblems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalCommonProblems");
        }
    }
}
