using DrustvenaMrezaFilmovi.Data;
using DrustvenaMrezaFilmovi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace DrustvenaMrezaFilmovi.Controllers
{
    public class FilmController : Controller
    {
        private readonly ILogger<FilmController> _logger;
        private readonly AppDbContext db;
        private static Film trenutniFilm;

        public FilmController(ILogger<FilmController> logger, AppDbContext _db)
        {
            _logger = logger;
            db = _db;

        }

        public IActionResult Details(int id)
        {
            // Fetch the movie with the specified ID from your data source.
            // You can use a service or access your database here.
            trenutniFilm = db.Filmovi
                         .Where(f => f.Id == id)
                         .Include(z => z.Zanrovi)
                         .SingleOrDefault();
            // Example: Movie movie = movieService.GetMovieById(id);

            if (trenutniFilm == null)
            {
                return NotFound(); // Return a 404 Not Found response if the movie is not found.
            }
            trenutniFilm.Recenzije = db.Recenzije.Include(r => r.Korisnik).Include(f=>f.Film).Where(r => r.FilmId == trenutniFilm.Id).OrderByDescending(r => r.VremeNastanka).ToList();
            trenutniFilm.Reziseri = db.ReziseriFilmova.Include(r => r.Umetnik).Where(r => r.FilmId == trenutniFilm.Id).ToList();
            trenutniFilm.Uloge = db.Uloge.Include(r => r.Glumac).Where(r => r.FilmId == trenutniFilm.Id).ToList();
            double sum = 0;
            int counter = 0;
            foreach (Recenzija rec in trenutniFilm.Recenzije)
            {
                sum += rec.BrojZvezdica;
                counter++;
            }
            trenutniFilm.ProsecnaOcena = sum / counter;
            dynamic mymodel = new ExpandoObject();
            if(LoginRegistracijaController.ulogovan) mymodel.Recenzija = DaLiPostojiOcena(trenutniFilm.Id);
            mymodel.Film = trenutniFilm;
            mymodel.Recenzije = trenutniFilm.Recenzije.Take(5);
            return View(mymodel); // Return a view to display the movie details.
        }

        [HttpPost]
        public async Task<IActionResult> Oceni(Recenzija recenzija)
        {
            if (recenzija.Komentar == null) recenzija.Komentar = "";
            if (recenzija.Komentar.Length <= 300)
            {
                try {
                    
                    Recenzija trenutna = DaLiPostojiOcena(recenzija.FilmId);

                    if (trenutna == null)
                    {
                        recenzija.KorisnikId = LoginRegistracijaController.ulogovaniKorisnik.Id;
                        recenzija.VremeNastanka = DateTime.Now;
                        db.Recenzije.Add(recenzija);
                        PromeniPreferencije(trenutniFilm, recenzija.BrojZvezdica);
                        db.SaveChanges();
                        
                    }
                    else 
                    {
                        PromeniPreferencijeRevert(trenutniFilm, trenutna.BrojZvezdica);
                        trenutna.BrojZvezdica = recenzija.BrojZvezdica;
                        trenutna.Komentar = recenzija.Komentar;
                        trenutna.VremeNastanka = DateTime.Now;
                        PromeniPreferencije(trenutniFilm, recenzija.BrojZvezdica);
                        db.SaveChanges();

                    }
                    return Json(new { success = true });
                }
                catch (Exception)
                {
                    return Json(new { success = false });
                }
            }
            return Json(new { success = false });
        }

        public Recenzija DaLiPostojiOcena(int filmId)
        {
            var recenzija = db.Recenzije
    .Include(r => r.Film) // Include the Film navigation property
    .Include(r => r.Korisnik) // Include the Korisnik navigation property
    .SingleOrDefault(r => r.Film.Id == filmId && r.Korisnik.Id == LoginRegistracijaController.ulogovaniKorisnik.Id);
            return recenzija;
        }

        public void PromeniPreferencije(Film f, double rating)
        {
            foreach (Uloga u in f.Uloge)
            {
                PreferencijaGlumac pg = db.PreferencijeGlumaca
                    .Where(k => k.KorisnikId == LoginRegistracijaController.ulogovaniKorisnik.Id && k.UmetnikId == u.GlumacId)
                    .SingleOrDefault();
                pg.VrednostPreferencije = pg.VrednostPreferencije * (0.9 + rating / 500);

            }
            foreach (ReziserFilma r in f.Reziseri)
            {
                PreferencijaReziser pr = db.PreferencijeRezisera
                    .Where(k => k.KorisnikId == LoginRegistracijaController.ulogovaniKorisnik.Id && k.UmetnikId == r.UmetnikId)
                    .SingleOrDefault();
                pr.VrednostPreferencije = pr.VrednostPreferencije * (0.9 + rating / 500);
            }
            foreach (ZanrFilma zf in f.Zanrovi)
            {
                PreferencijaZanr pz = db.PreferencijeZanrova
                        .Where(k => k.KorisnikId == LoginRegistracijaController.ulogovaniKorisnik.Id && k.Zanr == zf.Zanr)
                        .SingleOrDefault();
                pz.VrednostPreferencije = pz.VrednostPreferencije * (0.9 + rating / 500);
            }
            PreferencijaRegion preg = db.PreferencijeRegiona
                    .Where(k => k.KorisnikId == LoginRegistracijaController.ulogovaniKorisnik.Id && k.Region == f.Region)
                    .SingleOrDefault();
            preg.VrednostPreferencije = preg.VrednostPreferencije * (0.9 + rating / 500);
            
            for (int i = -4; i <= 4; i++)
            {
                PreferencijaGodinaNastanka pgod = db.PreferencijeGodinaNastanka
                    .Where(k =>
                            k.KorisnikId == LoginRegistracijaController.ulogovaniKorisnik.Id && k.GodinaNastanka == f.GodinaNastanka + i)
                    .SingleOrDefault();
                pgod.VrednostPreferencije =
                    pgod.VrednostPreferencije * (0.9 + Math.Abs(i)*0.02 + rating / 100 * (1 - (0.9 + Math.Abs(i)*0.02)) / 50);

            }

            PromeniSvePreferencijeKaFilmova();

        }

        public void PromeniPreferencijeRevert(Film f, double rating)
        {
            
            foreach (Uloga u in f.Uloge)
            {
                PreferencijaGlumac pg = db.PreferencijeGlumaca
                    .Where(k => k.KorisnikId == LoginRegistracijaController.ulogovaniKorisnik.Id && k.UmetnikId == u.GlumacId)
                    .SingleOrDefault();
                pg.VrednostPreferencije = pg.VrednostPreferencije / (0.9 + rating / 500);
                

            }
            foreach (ReziserFilma r in f.Reziseri)
            {
                PreferencijaReziser pr = db.PreferencijeRezisera
                    .Where(k => k.KorisnikId == LoginRegistracijaController.ulogovaniKorisnik.Id && k.UmetnikId == r.UmetnikId)
                    .SingleOrDefault();
                pr.VrednostPreferencije = pr.VrednostPreferencije / (0.9 + rating / 500);
                
            }
            foreach (ZanrFilma zf in f.Zanrovi)
            {
                PreferencijaZanr pr = db.PreferencijeZanrova
                        .Where(k => k.KorisnikId == LoginRegistracijaController.ulogovaniKorisnik.Id && k.Zanr == zf.Zanr)
                        .SingleOrDefault();
                pr.VrednostPreferencije = pr.VrednostPreferencije / (0.9 + rating / 500);
               
            }
            PreferencijaRegion preg = db.PreferencijeRegiona
                    .Where(k => k.KorisnikId == LoginRegistracijaController.ulogovaniKorisnik.Id && k.Region == f.Region)
                    .SingleOrDefault();
            preg.VrednostPreferencije = preg.VrednostPreferencije / (0.9 + rating / 500);
          
            for (int i = -4; i <= 4; i++)
            {
                PreferencijaGodinaNastanka pgod = db.PreferencijeGodinaNastanka
                    .Where(k =>
                            k.KorisnikId == LoginRegistracijaController.ulogovaniKorisnik.Id && k.GodinaNastanka == f.GodinaNastanka + i)
                    .SingleOrDefault();
                pgod.VrednostPreferencije =
                    pgod.VrednostPreferencije / (0.9 + Math.Abs(i) + rating / 100 * (1 - (0.9 + Math.Abs(i))) / 50);
                
            }
            
            PromeniSvePreferencijeKaFilmova();

        }

        public void PromeniSvePreferencijeKaFilmova()
        {
            List<Film> filmovi = db.Filmovi
                         .Include(z => z.Zanrovi)
                         .ToList();
            // Example: Movie movie = movieService.GetMovieById(id);

            foreach (Film trenutniFilm in filmovi)
            {
                trenutniFilm.Reziseri = db.ReziseriFilmova.Include(r => r.Umetnik).Where(r => r.FilmId == trenutniFilm.Id).ToList();
                trenutniFilm.Uloge = db.Uloge.Include(r => r.Glumac).Where(r => r.FilmId == trenutniFilm.Id).ToList();
            }

            foreach (Film f in filmovi)
            {
                List<double> prefGlumaca = new List<double>();
                List<double> prefRezisera = new List<double>();
                List<double> prefZanrova = new List<double>();
                double prefRegion;
                double prefGodina = 0;
                foreach (Uloga u in f.Uloge)
                {
                    PreferencijaGlumac pg = db.PreferencijeGlumaca
                        .Where(k => k.KorisnikId == LoginRegistracijaController.ulogovaniKorisnik.Id && k.UmetnikId == u.GlumacId)
                        .SingleOrDefault();
                    prefGlumaca.Add(pg.VrednostPreferencije);

                }
                foreach (ReziserFilma r in f.Reziseri)
                {
                    PreferencijaReziser pr = db.PreferencijeRezisera
                        .Where(k => k.KorisnikId == LoginRegistracijaController.ulogovaniKorisnik.Id && k.UmetnikId == r.UmetnikId)
                        .SingleOrDefault();

                    prefRezisera.Add(pr.VrednostPreferencije);
                }
                foreach (ZanrFilma zf in f.Zanrovi)
                {
                    PreferencijaZanr pr = db.PreferencijeZanrova
                            .Where(k => k.KorisnikId == LoginRegistracijaController.ulogovaniKorisnik.Id && k.Zanr == zf.Zanr)
                            .SingleOrDefault();
                    prefZanrova.Add(pr.VrednostPreferencije);
                }
                PreferencijaRegion preg = db.PreferencijeRegiona
                        .Where(k => k.KorisnikId == LoginRegistracijaController.ulogovaniKorisnik.Id && k.Region == f.Region)
                        .SingleOrDefault();
                prefRegion = preg.VrednostPreferencije;

                    PreferencijaGodinaNastanka pgod = db.PreferencijeGodinaNastanka
                        .Where(k =>
                                k.KorisnikId == LoginRegistracijaController.ulogovaniKorisnik.Id && k.GodinaNastanka == f.GodinaNastanka)
                        .SingleOrDefault();

                   
                        prefGodina = pgod.VrednostPreferencije;
                 

                PreferencijaFilm pfilm = db.PreferencijeFilm
                        .Where(k => k.KorisnikId == LoginRegistracijaController.ulogovaniKorisnik.Id && k.FilmId == f.Id)
                        .SingleOrDefault();
                pfilm.IzracunajPreferenciju(prefRegion, prefGodina, prefZanrova, prefGlumaca, prefRezisera);
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
