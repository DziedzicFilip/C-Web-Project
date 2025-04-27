using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firma.Data.Migrations
{
    /// <inheritdoc />
    public partial class ZmianaTowaruM5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kontakt",
                columns: table => new
                {
                    IdKonakt = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Biuro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KodPocztowy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Miasto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GodzinyPracy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kontakt", x => x.IdKonakt);
                });

            migrationBuilder.CreateTable(
                name: "Onas",
                columns: table => new
                {
                    IdOnas = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onas", x => x.IdOnas);
                });

            migrationBuilder.CreateTable(
                name: "PolitykaPrywatnosci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tytul = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tresc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataUtworzenia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Aktywna = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolitykaPrywatnosci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrzydatneLinki",
                columns: table => new
                {
                    IdPrzydatneLinki = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tytul = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrzydatneLinki", x => x.IdPrzydatneLinki);
                });

            migrationBuilder.CreateTable(
                name: "Regualmin",
                columns: table => new
                {
                    IdRegulamin = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tytul = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tresc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regualmin", x => x.IdRegulamin);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kontakt");

            migrationBuilder.DropTable(
                name: "Onas");

            migrationBuilder.DropTable(
                name: "PolitykaPrywatnosci");

            migrationBuilder.DropTable(
                name: "PrzydatneLinki");

            migrationBuilder.DropTable(
                name: "Regualmin");
        }
    }
}
