using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrustvenaMrezaFilmovi.Migrations
{
    /// <inheritdoc />
    public partial class DodatakolonaprihvacenoutabeluPrijateljstva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Prihvaceno",
                table: "Prijateljstva",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prihvaceno",
                table: "Prijateljstva");
        }
    }
}
