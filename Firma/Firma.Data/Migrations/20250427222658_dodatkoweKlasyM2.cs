using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firma.Data.Migrations
{
    /// <inheritdoc />
    public partial class dodatkoweKlasyM2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Haslo",
                table: "Uzytkownik",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Uzytkownik",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataPremiery",
                table: "Towar",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GwarancjaMiesiace",
                table: "Towar",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kolor",
                table: "Towar",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KrajProdukcji",
                table: "Towar",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "Towar",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Towar",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Producent",
                table: "Towar",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Stan",
                table: "Towar",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Waga",
                table: "Towar",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Wymiary",
                table: "Towar",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Haslo",
                table: "Uzytkownik");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Uzytkownik");

            migrationBuilder.DropColumn(
                name: "DataPremiery",
                table: "Towar");

            migrationBuilder.DropColumn(
                name: "GwarancjaMiesiace",
                table: "Towar");

            migrationBuilder.DropColumn(
                name: "Kolor",
                table: "Towar");

            migrationBuilder.DropColumn(
                name: "KrajProdukcji",
                table: "Towar");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "Towar");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Towar");

            migrationBuilder.DropColumn(
                name: "Producent",
                table: "Towar");

            migrationBuilder.DropColumn(
                name: "Stan",
                table: "Towar");

            migrationBuilder.DropColumn(
                name: "Waga",
                table: "Towar");

            migrationBuilder.DropColumn(
                name: "Wymiary",
                table: "Towar");
        }
    }
}
