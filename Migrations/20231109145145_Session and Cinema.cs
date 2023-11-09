using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmesApi.Migrations
{
    /// <inheritdoc />
    public partial class SessionandCinema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CinemaId",
                table: "Session",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Session_CinemaId",
                table: "Session",
                column: "CinemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Cinemas_CinemaId",
                table: "Session",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_Cinemas_CinemaId",
                table: "Session");

            migrationBuilder.DropIndex(
                name: "IX_Session_CinemaId",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "CinemaId",
                table: "Session");
        }
    }
}
