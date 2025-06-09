using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firma.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationUserIdToUzytkownik : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Uzytkownik",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Uzytkownik_ApplicationUserId",
                table: "Uzytkownik",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Uzytkownik_AspNetUsers_ApplicationUserId",
                table: "Uzytkownik",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uzytkownik_AspNetUsers_ApplicationUserId",
                table: "Uzytkownik");

            migrationBuilder.DropIndex(
                name: "IX_Uzytkownik_ApplicationUserId",
                table: "Uzytkownik");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Uzytkownik");
        }
    }
}
