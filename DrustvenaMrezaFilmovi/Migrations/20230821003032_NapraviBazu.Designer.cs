﻿// <auto-generated />
using System;
using DrustvenaMrezaFilmovi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DrustvenaMrezaFilmovi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230821003032_NapraviBazu")]
    partial class NapraviBazu
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GodinaNastanka")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Region")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Filmovi");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.Korisnik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Godiste")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pol")
                        .HasColumnType("int");

                    b.Property<int>("Region")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Korisnici");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.PreferencijaGlumac", b =>
                {
                    b.Property<int>("KorisnikId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int>("UmetnikId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.Property<double>("VrednostPreferencije")
                        .HasColumnType("float");

                    b.HasKey("KorisnikId", "UmetnikId");

                    b.HasIndex("UmetnikId");

                    b.ToTable("PreferencijeGlumaca");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.PreferencijaGodinaNastanka", b =>
                {
                    b.Property<int>("GodinaNastanka")
                        .HasColumnType("int");

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<double>("VrednostPreferencije")
                        .HasColumnType("float");

                    b.HasKey("GodinaNastanka", "KorisnikId");

                    b.HasIndex("KorisnikId");

                    b.ToTable("PreferencijeGodinaNastanka");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.PreferencijaRegion", b =>
                {
                    b.Property<int>("Region")
                        .HasColumnType("int");

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<double>("VrednostPreferencije")
                        .HasColumnType("float");

                    b.HasKey("Region", "KorisnikId");

                    b.HasIndex("KorisnikId");

                    b.ToTable("PreferencijeRegiona");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.PreferencijaReziser", b =>
                {
                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<int>("UmetnikId")
                        .HasColumnType("int");

                    b.Property<double>("VrednostPreferencije")
                        .HasColumnType("float");

                    b.HasKey("KorisnikId", "UmetnikId");

                    b.HasIndex("UmetnikId");

                    b.ToTable("PreferencijeRezisera");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.PreferencijaZanr", b =>
                {
                    b.Property<int>("Zanr")
                        .HasColumnType("int");

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<double>("VrednostPreferencije")
                        .HasColumnType("float");

                    b.HasKey("Zanr", "KorisnikId");

                    b.HasIndex("KorisnikId");

                    b.ToTable("PreferencijeZanrova");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.Prijateljstvo", b =>
                {
                    b.Property<int>("KorisnikId1")
                        .HasColumnType("int");

                    b.Property<int>("KorisnikId2")
                        .HasColumnType("int");

                    b.Property<DateTime>("PocetakPrijateljstva")
                        .HasColumnType("datetime2");

                    b.Property<double>("Uticaj1")
                        .HasColumnType("float");

                    b.Property<double>("Uticaj2")
                        .HasColumnType("float");

                    b.HasKey("KorisnikId1", "KorisnikId2");

                    b.HasIndex("KorisnikId2");

                    b.ToTable("Prijateljstva");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.Recenzija", b =>
                {
                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<int>("BrojZvezdica")
                        .HasColumnType("int");

                    b.Property<string>("Komentar")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("FilmId", "KorisnikId");

                    b.HasIndex("KorisnikId");

                    b.ToTable("Recenzije");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.ReziserFilma", b =>
                {
                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.Property<int>("UmetnikId")
                        .HasColumnType("int");

                    b.HasKey("FilmId", "UmetnikId");

                    b.HasIndex("UmetnikId");

                    b.ToTable("ReziseriFilmova");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.Uloga", b =>
                {
                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.Property<int>("GlumacId")
                        .HasColumnType("int");

                    b.Property<string>("ImeUloge")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("JeGlavna")
                        .HasColumnType("bit");

                    b.HasKey("FilmId", "GlumacId");

                    b.HasIndex("GlumacId");

                    b.ToTable("Uloge");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.Umetnik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Godiste")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pol")
                        .HasColumnType("int");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Region")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Umetnici");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.ZanrFilma", b =>
                {
                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.Property<int>("Zanr")
                        .HasColumnType("int");

                    b.HasKey("FilmId", "Zanr");

                    b.ToTable("ZanroviFilmova");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.PreferencijaGlumac", b =>
                {
                    b.HasOne("DrustvenaMrezaFilmovi.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DrustvenaMrezaFilmovi.Models.Umetnik", "Umetnik")
                        .WithMany()
                        .HasForeignKey("UmetnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");

                    b.Navigation("Umetnik");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.PreferencijaGodinaNastanka", b =>
                {
                    b.HasOne("DrustvenaMrezaFilmovi.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.PreferencijaRegion", b =>
                {
                    b.HasOne("DrustvenaMrezaFilmovi.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.PreferencijaReziser", b =>
                {
                    b.HasOne("DrustvenaMrezaFilmovi.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DrustvenaMrezaFilmovi.Models.Umetnik", "Umetnik")
                        .WithMany()
                        .HasForeignKey("UmetnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");

                    b.Navigation("Umetnik");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.PreferencijaZanr", b =>
                {
                    b.HasOne("DrustvenaMrezaFilmovi.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.Prijateljstvo", b =>
                {
                    b.HasOne("DrustvenaMrezaFilmovi.Models.Korisnik", "Korisnik1")
                        .WithMany()
                        .HasForeignKey("KorisnikId1")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DrustvenaMrezaFilmovi.Models.Korisnik", "Korisnik2")
                        .WithMany()
                        .HasForeignKey("KorisnikId2")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Korisnik1");

                    b.Navigation("Korisnik2");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.Recenzija", b =>
                {
                    b.HasOne("DrustvenaMrezaFilmovi.Models.Film", "Film")
                        .WithMany()
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DrustvenaMrezaFilmovi.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.ReziserFilma", b =>
                {
                    b.HasOne("DrustvenaMrezaFilmovi.Models.Film", "Film")
                        .WithMany("Reziseri")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DrustvenaMrezaFilmovi.Models.Umetnik", "Umetnik")
                        .WithMany()
                        .HasForeignKey("UmetnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("Umetnik");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.Uloga", b =>
                {
                    b.HasOne("DrustvenaMrezaFilmovi.Models.Film", "Film")
                        .WithMany("Uloge")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DrustvenaMrezaFilmovi.Models.Umetnik", "Glumac")
                        .WithMany()
                        .HasForeignKey("GlumacId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("Glumac");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.ZanrFilma", b =>
                {
                    b.HasOne("DrustvenaMrezaFilmovi.Models.Film", "Film")
                        .WithMany()
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");
                });

            modelBuilder.Entity("DrustvenaMrezaFilmovi.Models.Film", b =>
                {
                    b.Navigation("Reziseri");

                    b.Navigation("Uloge");
                });
#pragma warning restore 612, 618
        }
    }
}
