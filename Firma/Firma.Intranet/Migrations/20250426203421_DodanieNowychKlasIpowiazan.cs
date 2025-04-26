using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firma.Intranet.Migrations
{
    /// <inheritdoc />
    public partial class DodanieNowychKlasIpowiazan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promocja",
                columns: table => new
                {
                    IdPromocji = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rabat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataRozpoczecia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataZakonczenia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CzyAktywna = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocja", x => x.IdPromocji);
                });

            migrationBuilder.CreateTable(
                name: "ProduktPromocja",
                columns: table => new
                {
                    IdProduktyPromocji = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTowaru = table.Column<int>(type: "int", nullable: false),
                    IdPromocji = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduktPromocja", x => x.IdProduktyPromocji);
                    table.ForeignKey(
                        name: "FK_ProduktPromocja_Promocja_IdPromocji",
                        column: x => x.IdPromocji,
                        principalTable: "Promocja",
                        principalColumn: "IdPromocji",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProduktPromocja_Towar_IdTowaru",
                        column: x => x.IdTowaru,
                        principalTable: "Towar",
                        principalColumn: "idTowar",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProduktPromocja_IdPromocji",
                table: "ProduktPromocja",
                column: "IdPromocji");

            migrationBuilder.CreateIndex(
                name: "IX_ProduktPromocja_IdTowaru",
                table: "ProduktPromocja",
                column: "IdTowaru");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProduktPromocja");

            migrationBuilder.DropTable(
                name: "Promocja");
        }
    }
}
