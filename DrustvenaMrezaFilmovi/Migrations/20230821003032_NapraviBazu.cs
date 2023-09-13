using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrustvenaMrezaFilmovi.Migrations
{
    /// <inheritdoc />
    public partial class NapraviBazu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filmovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GodinaNastanka = table.Column<int>(type: "int", nullable: false),
                    Region = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmovi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Godiste = table.Column<int>(type: "int", nullable: false),
                    Pol = table.Column<int>(type: "int", nullable: false),
                    Region = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Umetnici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pol = table.Column<int>(type: "int", nullable: false),
                    Godiste = table.Column<int>(type: "int", nullable: false),
                    Region = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Umetnici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZanroviFilmova",
                columns: table => new
                {
                    Zanr = table.Column<int>(type: "int", nullable: false),
                    FilmId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZanroviFilmova", x => new { x.FilmId, x.Zanr });
                    table.ForeignKey(
                        name: "FK_ZanroviFilmova_Filmovi_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Filmovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreferencijeGodinaNastanka",
                columns: table => new
                {
                    GodinaNastanka = table.Column<int>(type: "int", nullable: false),
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    VrednostPreferencije = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferencijeGodinaNastanka", x => new { x.GodinaNastanka, x.KorisnikId });
                    table.ForeignKey(
                        name: "FK_PreferencijeGodinaNastanka_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreferencijeRegiona",
                columns: table => new
                {
                    Region = table.Column<int>(type: "int", nullable: false),
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    VrednostPreferencije = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferencijeRegiona", x => new { x.Region, x.KorisnikId });
                    table.ForeignKey(
                        name: "FK_PreferencijeRegiona_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreferencijeZanrova",
                columns: table => new
                {
                    Zanr = table.Column<int>(type: "int", nullable: false),
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    VrednostPreferencije = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferencijeZanrova", x => new { x.Zanr, x.KorisnikId });
                    table.ForeignKey(
                        name: "FK_PreferencijeZanrova_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prijateljstva",
                columns: table => new
                {
                    KorisnikId1 = table.Column<int>(type: "int", nullable: false),
                    KorisnikId2 = table.Column<int>(type: "int", nullable: false),
                    PocetakPrijateljstva = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Uticaj1 = table.Column<double>(type: "float", nullable: false),
                    Uticaj2 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prijateljstva", x => new { x.KorisnikId1, x.KorisnikId2 });
                    table.ForeignKey(
                        name: "FK_Prijateljstva_Korisnici_KorisnikId1",
                        column: x => x.KorisnikId1,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prijateljstva_Korisnici_KorisnikId2",
                        column: x => x.KorisnikId2,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recenzije",
                columns: table => new
                {
                    FilmId = table.Column<int>(type: "int", nullable: false),
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    BrojZvezdica = table.Column<int>(type: "int", nullable: false),
                    Komentar = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzije", x => new { x.FilmId, x.KorisnikId });
                    table.ForeignKey(
                        name: "FK_Recenzije_Filmovi_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Filmovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recenzije_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreferencijeGlumaca",
                columns: table => new
                {
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    UmetnikId = table.Column<int>(type: "int", nullable: false),
                    VrednostPreferencije = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferencijeGlumaca", x => new { x.KorisnikId, x.UmetnikId });
                    table.ForeignKey(
                        name: "FK_PreferencijeGlumaca_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreferencijeGlumaca_Umetnici_UmetnikId",
                        column: x => x.UmetnikId,
                        principalTable: "Umetnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreferencijeRezisera",
                columns: table => new
                {
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    UmetnikId = table.Column<int>(type: "int", nullable: false),
                    VrednostPreferencije = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferencijeRezisera", x => new { x.KorisnikId, x.UmetnikId });
                    table.ForeignKey(
                        name: "FK_PreferencijeRezisera_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreferencijeRezisera_Umetnici_UmetnikId",
                        column: x => x.UmetnikId,
                        principalTable: "Umetnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReziseriFilmova",
                columns: table => new
                {
                    FilmId = table.Column<int>(type: "int", nullable: false),
                    UmetnikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReziseriFilmova", x => new { x.FilmId, x.UmetnikId });
                    table.ForeignKey(
                        name: "FK_ReziseriFilmova_Filmovi_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Filmovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReziseriFilmova_Umetnici_UmetnikId",
                        column: x => x.UmetnikId,
                        principalTable: "Umetnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uloge",
                columns: table => new
                {
                    FilmId = table.Column<int>(type: "int", nullable: false),
                    GlumacId = table.Column<int>(type: "int", nullable: false),
                    ImeUloge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JeGlavna = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uloge", x => new { x.FilmId, x.GlumacId });
                    table.ForeignKey(
                        name: "FK_Uloge_Filmovi_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Filmovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uloge_Umetnici_GlumacId",
                        column: x => x.GlumacId,
                        principalTable: "Umetnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreferencijeGlumaca_UmetnikId",
                table: "PreferencijeGlumaca",
                column: "UmetnikId");

            migrationBuilder.CreateIndex(
                name: "IX_PreferencijeGodinaNastanka_KorisnikId",
                table: "PreferencijeGodinaNastanka",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_PreferencijeRegiona_KorisnikId",
                table: "PreferencijeRegiona",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_PreferencijeRezisera_UmetnikId",
                table: "PreferencijeRezisera",
                column: "UmetnikId");

            migrationBuilder.CreateIndex(
                name: "IX_PreferencijeZanrova_KorisnikId",
                table: "PreferencijeZanrova",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Prijateljstva_KorisnikId2",
                table: "Prijateljstva",
                column: "KorisnikId2");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_KorisnikId",
                table: "Recenzije",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_ReziseriFilmova_UmetnikId",
                table: "ReziseriFilmova",
                column: "UmetnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Uloge_GlumacId",
                table: "Uloge",
                column: "GlumacId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreferencijeGlumaca");

            migrationBuilder.DropTable(
                name: "PreferencijeGodinaNastanka");

            migrationBuilder.DropTable(
                name: "PreferencijeRegiona");

            migrationBuilder.DropTable(
                name: "PreferencijeRezisera");

            migrationBuilder.DropTable(
                name: "PreferencijeZanrova");

            migrationBuilder.DropTable(
                name: "Prijateljstva");

            migrationBuilder.DropTable(
                name: "Recenzije");

            migrationBuilder.DropTable(
                name: "ReziseriFilmova");

            migrationBuilder.DropTable(
                name: "Uloge");

            migrationBuilder.DropTable(
                name: "ZanroviFilmova");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Umetnici");

            migrationBuilder.DropTable(
                name: "Filmovi");
        }
    }
}
