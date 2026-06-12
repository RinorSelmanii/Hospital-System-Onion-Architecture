using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitializeSpitaliDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pacientet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mbiemri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumriPersonal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataLindjes = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repartet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmriRepartit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lokacioni = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repartet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mjeket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mbiemri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specializimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepartiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mjeket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mjeket_Repartet_RepartiId",
                        column: x => x.RepartiId,
                        principalTable: "Repartet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Terminet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataTerminit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Diagnoza = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MjekuId = table.Column<int>(type: "int", nullable: false),
                    PacientiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Terminet_Mjeket_MjekuId",
                        column: x => x.MjekuId,
                        principalTable: "Mjeket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Terminet_Pacientet_PacientiId",
                        column: x => x.PacientiId,
                        principalTable: "Pacientet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mjeket_RepartiId",
                table: "Mjeket",
                column: "RepartiId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminet_MjekuId",
                table: "Terminet",
                column: "MjekuId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminet_PacientiId",
                table: "Terminet",
                column: "PacientiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Terminet");

            migrationBuilder.DropTable(
                name: "Mjeket");

            migrationBuilder.DropTable(
                name: "Pacientet");

            migrationBuilder.DropTable(
                name: "Repartet");
        }
    }
}
