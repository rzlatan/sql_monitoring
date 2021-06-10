using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitoring.Migrations
{
    public partial class morebasicinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BasicInformationStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServerVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Edition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessorCount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Memory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkerCount = table.Column<int>(type: "int", nullable: false),
                    CompatLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Collation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsXTPEnabled = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHADREnabled = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HyperThreadRatio = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicInformationStats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Database",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatabaseSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndexSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReserverSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnusedSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicInformationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Database", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Database_BasicInformationStats_BasicInformationId",
                        column: x => x.BasicInformationId,
                        principalTable: "BasicInformationStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DatabaseFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<double>(type: "float", nullable: false),
                    MaxSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutoGrowth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicInformationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatabaseFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatabaseFile_BasicInformationStats_BasicInformationId",
                        column: x => x.BasicInformationId,
                        principalTable: "BasicInformationStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Database_BasicInformationId",
                table: "Database",
                column: "BasicInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseFile_BasicInformationId",
                table: "DatabaseFile",
                column: "BasicInformationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Database");

            migrationBuilder.DropTable(
                name: "DatabaseFile");

            migrationBuilder.DropTable(
                name: "BasicInformationStats");
        }
    }
}
