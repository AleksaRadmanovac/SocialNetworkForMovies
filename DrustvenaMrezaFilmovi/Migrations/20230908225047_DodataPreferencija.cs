using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrustvenaMrezaFilmovi.Migrations
{
    /// <inheritdoc />
    public partial class DodataPreferencija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PreferencijeFilm",
                columns: table => new
                {
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    FilmId = table.Column<int>(type: "int", nullable: false),
                    VrednostPreferencije = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferencijeFilm", x => new { x.KorisnikId, x.FilmId });
                    table.ForeignKey(
                        name: "FK_PreferencijeFilm_Filmovi_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Filmovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreferencijeFilm_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreferencijeFilm_FilmId",
                table: "PreferencijeFilm",
                column: "FilmId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreferencijeFilm");
        }
    }
}
