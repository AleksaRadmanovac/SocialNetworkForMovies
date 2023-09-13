using DrustvenaMrezaFilmovi.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace DrustvenaMrezaFilmovi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            SeedDefaultData();
        }

        public DbSet<Film> Filmovi { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<PreferencijaGlumac> PreferencijeGlumaca { get; set; }
        public DbSet<PreferencijaFilm> PreferencijeFilm { get; set; }
        public DbSet<PreferencijaGodinaNastanka> PreferencijeGodinaNastanka { get; set; }
        public DbSet<PreferencijaRegion> PreferencijeRegiona { get; set; }
        public DbSet<PreferencijaReziser> PreferencijeRezisera { get; set; }
        public DbSet<PreferencijaZanr> PreferencijeZanrova { get; set; }
        public DbSet<Prijateljstvo> Prijateljstva { get; set; }
        public DbSet<Recenzija> Recenzije { get; set; }
        public DbSet<ReziserFilma> ReziseriFilmova { get; set; }
        public DbSet<Uloga> Uloge { get; set; }
        public DbSet<Umetnik> Umetnici { get; set; }
        public DbSet<ZanrFilma> ZanroviFilmova { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PreferencijaGlumac>()
                .HasKey(pg => new { pg.KorisnikId, pg.UmetnikId });

            modelBuilder.Entity<PreferencijaGlumac>()
                .HasOne(pg => pg.Korisnik)
                .WithMany()
                .HasForeignKey(pg => pg.KorisnikId);

            modelBuilder.Entity<PreferencijaGlumac>()
                .HasOne(pg => pg.Umetnik)
                .WithMany()
                .HasForeignKey(pg => pg.UmetnikId);

            modelBuilder.Entity<PreferencijaFilm>()
                .HasKey(pg => new { pg.KorisnikId, pg.FilmId });

            modelBuilder.Entity<PreferencijaFilm>()
                .HasOne(pg => pg.Korisnik)
                .WithMany()
                .HasForeignKey(pg => pg.KorisnikId);

            modelBuilder.Entity<PreferencijaFilm>()
                .HasOne(pg => pg.Film)
                .WithMany()
                .HasForeignKey(pg => pg.FilmId);

            modelBuilder.Entity<PreferencijaGodinaNastanka>()
                .HasKey(pg => new { pg.GodinaNastanka, pg.KorisnikId });

            modelBuilder.Entity<PreferencijaGodinaNastanka>()
                .HasOne(pg => pg.Korisnik)
                .WithMany()
                .HasForeignKey(pg => pg.KorisnikId);

            modelBuilder.Entity<PreferencijaRegion>()
                .HasKey(pr => new { pr.Region, pr.KorisnikId });

            modelBuilder.Entity<PreferencijaRegion>()
                .HasOne(pr => pr.Korisnik)
                .WithMany()
                .HasForeignKey(pr => pr.KorisnikId);

            modelBuilder.Entity<ZanrFilma>()
            .HasKey(zf => new { zf.FilmId, zf.Zanr });

            modelBuilder.Entity<ZanrFilma>()
                .HasOne(zf => zf.Film)
                .WithMany()
                .HasForeignKey(zf => zf.FilmId);

            modelBuilder.Entity<PreferencijaReziser>()
            .HasKey(pr => new { pr.KorisnikId, pr.UmetnikId });

            modelBuilder.Entity<PreferencijaReziser>()
                .HasOne(pr => pr.Korisnik)
                .WithMany()
                .HasForeignKey(pr => pr.KorisnikId);

            modelBuilder.Entity<PreferencijaReziser>()
                .HasOne(pr => pr.Umetnik)
                .WithMany()
                .HasForeignKey(pr => pr.UmetnikId);

            modelBuilder.Entity<PreferencijaZanr>()
            .HasKey(pz => new { pz.Zanr, pz.KorisnikId });

            modelBuilder.Entity<PreferencijaZanr>()
                .HasOne(pz => pz.Korisnik)
                .WithMany()
                .HasForeignKey(pz => pz.KorisnikId);

            modelBuilder.Entity<Prijateljstvo>()
                .HasKey(p => new { p.KorisnikId1, p.KorisnikId2 });

            modelBuilder.Entity<Prijateljstvo>()
                .HasOne(p => p.Korisnik1)
                .WithMany()
                .HasForeignKey(p => p.KorisnikId1)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Prijateljstvo>()
                .HasOne(p => p.Korisnik2)
                .WithMany()
                .HasForeignKey(p => p.KorisnikId2)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Recenzija>()
           .HasKey(r => new { r.FilmId, r.KorisnikId });

            modelBuilder.Entity<Recenzija>()
                .HasOne(r => r.Film)
                .WithMany()
                .HasForeignKey(r => r.FilmId);

            modelBuilder.Entity<Recenzija>()
                .HasOne(r => r.Korisnik)
                .WithMany()
                .HasForeignKey(r => r.KorisnikId);

            modelBuilder.Entity<ReziserFilma>()
                .HasKey(rf => new { rf.FilmId, rf.UmetnikId });

            modelBuilder.Entity<ReziserFilma>()
                .HasOne(rf => rf.Film)
                .WithMany(f => f.Reziseri)
                .HasForeignKey(rf => rf.FilmId);

            modelBuilder.Entity<ReziserFilma>()
                .HasOne(rf => rf.Umetnik)
                .WithMany()
                .HasForeignKey(rf => rf.UmetnikId);

            modelBuilder.Entity<Uloga>()
                .HasKey(u => new { u.FilmId, u.GlumacId });

            modelBuilder.Entity<Uloga>()
                .HasOne(u => u.Film)
                .WithMany(f => f.Uloge)
                .HasForeignKey(u => u.FilmId);

            modelBuilder.Entity<Uloga>()
                .HasOne(u => u.Glumac)
                .WithMany()
                .HasForeignKey(u => u.GlumacId);

            modelBuilder.Entity<ZanrFilma>()
            .HasKey(zf => new { zf.FilmId, zf.Zanr });

            modelBuilder.Entity<ZanrFilma>()
                .HasOne(zf => zf.Film)
                .WithMany(f => f.Zanrovi) // Assuming 'Zanrovi' is the navigation property in your Film class
                .HasForeignKey(zf => zf.FilmId);

        }
        public void SeedDefaultData()
        {
            var users = Korisnici.ToList();
            foreach (var user in users)
            {
                // Check if there is already data in the PreferenceTowardsMovie table
                if (!PreferencijeFilm.Where(p => p.KorisnikId == user.Id).Any())
                {
                    // Get all users and movies from their respective DbSet properties

                    var movies = Filmovi.ToList();

                    // Loop through each user and movie combination

                    foreach (var movie in movies)
                    {
                        // Create a new PreferenceTowardsMovie record with default value
                        var preferencija = new PreferencijaFilm
                        {
                            KorisnikId = user.Id,
                            FilmId = movie.Id,
                            VrednostPreferencije = 10 // Set your default value here
                        };

                        // Add to DbSet (PreferenceTowardsMovies) and save changes
                        PreferencijeFilm.Add(preferencija);
                    }


                }
                // Check if the PreferenceTowardsGenre table is empty
                if (!PreferencijeZanrova.Where(p => p.KorisnikId == user.Id).Any())
                {
                    // Get all users and genres

                    var genres = Enum.GetValues(typeof(ZanrEnum));

                    // Loop through each user and each genre to set default values
                    
                        foreach (var genre in genres)
                        {
                            var preferencija = new PreferencijaZanr
                            {
                                KorisnikId = user.Id,
                                Zanr = (ZanrEnum)genre,
                                VrednostPreferencije = 10 // Set your default value
                            };

                            PreferencijeZanrova.Add(preferencija);
                        }
                    

                    // Save changes to the database

                }
                // Check if the PreferenceTowardsGenre table is empty
                if (!PreferencijeRegiona.Where(p => p.KorisnikId == user.Id).Any())
                {
                    // Get all users and genres
                    var genres = Enum.GetValues(typeof(Region));

                    // Loop through each user and each genre to set default values
                    
                        foreach (var genre in genres)
                        {
                            var preferencija = new PreferencijaRegion
                            {
                                KorisnikId = user.Id,
                                Region = (Region)genre,
                                VrednostPreferencije = 10 // Set your default value
                            };

                            PreferencijeRegiona.Add(preferencija);
                        }
                    

                    // Save changes to the database

                }
                if (!PreferencijeGlumaca.Where(p => p.KorisnikId == user.Id).Any())
                {
                    // Get all users and movies from their respective DbSet properties
                    var movies = Umetnici.ToList();

                    // Loop through each user and movie combination
                    
                        foreach (var movie in movies)
                        {
                            // Create a new PreferenceTowardsMovie record with default value
                            var preferencija = new PreferencijaGlumac
                            {
                                KorisnikId = user.Id,
                                UmetnikId = movie.Id,
                                VrednostPreferencije = 10 // Set your default value here
                            };

                            // Add to DbSet (PreferenceTowardsMovies) and save changes
                            PreferencijeGlumaca.Add(preferencija);
                        }
                    

                }
                if (!PreferencijeRezisera.Where(p => p.KorisnikId == user.Id).Any())
                {
                    // Get all users and movies from their respective DbSet properties
                    
                    var movies = Umetnici.ToList();

                    // Loop through each user and movie combination
                    
                        foreach (var movie in movies)
                        {
                            // Create a new PreferenceTowardsMovie record with default value
                            var preferencija = new PreferencijaReziser
                            {
                                KorisnikId = user.Id,
                                UmetnikId = movie.Id,
                                VrednostPreferencije = 10 // Set your default value here
                            };

                            // Add to DbSet (PreferenceTowardsMovies) and save changes
                            PreferencijeRezisera.Add(preferencija);
                        }
                    

                }
                if (!PreferencijeGodinaNastanka.Where(p => p.KorisnikId == user.Id).Any())
                {
                    // Get all users and genres
                    

                    // Loop through each user and each genre to set default values
                    
                        for (int year = 1903; year <= 2023; year++)
                        {
                            var preferencija = new PreferencijaGodinaNastanka
                            {
                                KorisnikId = user.Id,
                                GodinaNastanka = year,
                                VrednostPreferencije = 10 // Set your default value
                            };

                            PreferencijeGodinaNastanka.Add(preferencija);
                        }
                    

                    // Save changes to the database

                }
            }
            SaveChanges();
        }

        public void DodajDefaultZaNovogUsera(int id)
        {
            // Check if there is already data in the PreferenceTowardsMovie table
                // Get all users and movies from their respective DbSet properties
               ;
                var movies = Filmovi.ToList();

                // Loop through each user and movie combination
                
                    foreach (var movie in movies)
                    {
                        // Create a new PreferenceTowardsMovie record with default value
                        var preferencija = new PreferencijaFilm
                        {
                            KorisnikId = id,
                            FilmId = movie.Id,
                            VrednostPreferencije = 10 // Set your default value here
                        };

                        // Add to DbSet (PreferenceTowardsMovies) and save changes
                        PreferencijeFilm.Add(preferencija);
                    }
                

            
            // Check if the PreferenceTowardsGenre table is empty
           
                // Get all users and genres
                
                var genres = Enum.GetValues(typeof(ZanrEnum));

                // Loop through each user and each genre to set default values
               
                    foreach (var genre in genres)
                    {
                        var preferencija = new PreferencijaZanr
                        {
                            KorisnikId = id,
                            Zanr = (ZanrEnum)genre,
                            VrednostPreferencije = 10 // Set your default value
                        };

                        PreferencijeZanrova.Add(preferencija);
                    }
                

                // Save changes to the database

            
            
                // Get all users and genres
                
                var regioni = Enum.GetValues(typeof(Region));

                // Loop through each user and each genre to set default values
               
                    foreach (var genre in regioni)
                    {
                        var preferencija = new PreferencijaRegion
                        {
                            KorisnikId = id,
                            Region = (Region)genre,
                            VrednostPreferencije = 10 // Set your default value
                        };

                        PreferencijeRegiona.Add(preferencija);
                    }
                

                // Save changes to the database

            
            
                // Get all users and movies from their respective DbSet properties
               
                var glumci = Umetnici.ToList();

                // Loop through each user and movie combination
               
                    foreach (var movie in glumci)
                    {
                        // Create a new PreferenceTowardsMovie record with default value
                        var preferencija = new PreferencijaGlumac
                        {
                            KorisnikId = id,
                            UmetnikId = movie.Id,
                            VrednostPreferencije = 10 // Set your default value here
                        };

                        // Add to DbSet (PreferenceTowardsMovies) and save changes
                        PreferencijeGlumaca.Add(preferencija);
                    }
               

            
            
                // Get all users and movies from their respective DbSet properties
                var users = Korisnici.ToList();
                var reziseri = Umetnici.ToList();

                // Loop through each user and movie combination
                
                    foreach (var movie in reziseri)
                    {
                        // Create a new PreferenceTowardsMovie record with default value
                        var preferencija = new PreferencijaReziser
                        {
                            KorisnikId = id,
                            UmetnikId = movie.Id,
                            VrednostPreferencije = 10 // Set your default value here
                        };

                        // Add to DbSet (PreferenceTowardsMovies) and save changes
                        PreferencijeRezisera.Add(preferencija);
                    }
                

            
            
                // Get all users and genres
                
                    for (int year = 1903; year <= 2023; year++)
                    {
                        var preferencija = new PreferencijaGodinaNastanka
                        {
                            KorisnikId = id,
                            GodinaNastanka = year,
                            VrednostPreferencije = 10 // Set your default value
                        };

                        PreferencijeGodinaNastanka.Add(preferencija);
                    }
                
        }

        public IEnumerable<Film> VratiFilmoveSaPreferencijamaSortirano(int userId)
        {
            var query = from film in Filmovi
                        join PreferencijaFilm in PreferencijeFilm
                        on film.Id equals PreferencijaFilm.FilmId
                        where PreferencijaFilm.FilmId == userId
                        orderby PreferencijaFilm.VrednostPreferencije descending
                        select film;
            IEnumerable<Film> lista = query.ToList();
            return lista;
        }


    }
}
