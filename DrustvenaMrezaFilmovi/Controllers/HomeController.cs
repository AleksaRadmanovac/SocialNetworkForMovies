using DrustvenaMrezaFilmovi.Data;
using DrustvenaMrezaFilmovi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Dynamic;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;

namespace DrustvenaMrezaFilmovi.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext db;
        //public static Korisnik ulogovaniKorisnik = null;
        //public static bool ulogovan = false;

        public HomeController(ILogger<HomeController> logger, AppDbContext _db)
        {
            _logger = logger;
            db = _db;

        }

        public IActionResult Index()
        {
            IEnumerable<Recenzija> latestRecenzije = db.Recenzije
               .Include(r => r.Film)
               .Include(r => r.Korisnik)
               .OrderByDescending(r => r.VremeNastanka)
               .Take(6);
            IEnumerable<Film> filmovi = db.Filmovi.ToList();

            foreach (Film film in filmovi)
            {
                film.Recenzije = db.Recenzije.Where(r => r.FilmId == film.Id).ToList();
                film.Reziseri = db.ReziseriFilmova.Include(r => r.Umetnik).Where(r => r.FilmId == film.Id).ToList();
                film.Uloge = db.Uloge.Include(r => r.Glumac).Where(r => r.FilmId == film.Id).ToList();
                double sum = 0;
                int counter = 0;
                foreach (Recenzija rec in film.Recenzije)
                {
                    sum += rec.BrojZvezdica;
                    counter++;
                }
                film.ProsecnaOcena = sum / counter;
            }
            filmovi = filmovi
            .OrderByDescending(film => film.ProsecnaOcena)
            .Take(5)
            .ToList();



            dynamic mymodel = new ExpandoObject();
            mymodel.Recenzije = latestRecenzije;
            mymodel.Filmovi = filmovi;
            ViewBag.SearchSuggestions = GetSearchSuggestions();
            return View(mymodel);
        }

        [Authorize]
        public IActionResult LoggedIn()
        {
            var friendIds = LoginRegistracijaController.ulogovaniKorisnik.Prijatelji.Select(k => k.Id).ToList();

            IEnumerable<Recenzija> latestRecenzije = db.Recenzije
                   .Include(r => r.Film)
                   .Include(r => r.Korisnik)
                   .Where(r => r.Korisnik.Id == LoginRegistracijaController.ulogovaniKorisnik.Id 
                   || friendIds.Contains(r.Korisnik.Id))
                   .OrderByDescending(r => r.VremeNastanka)
                   .Take(6);

            IEnumerable<Film> filmovi;
            filmovi = db.Filmovi
        .Join(
        db.PreferencijeFilm,
        film => film.Id,
        preferencijaFilm => preferencijaFilm.FilmId,
        (film, preferencijaFilm) => new { Film = film, PreferencijaFilm = preferencijaFilm })
    .Where(joinResult => joinResult.PreferencijaFilm.KorisnikId == LoginRegistracijaController.ulogovaniKorisnik.Id)
    .OrderByDescending(joinResult => joinResult.PreferencijaFilm.VrednostPreferencije)
    .Select(joinResult => joinResult.Film)
    .Take(6)
    .ToList();
            foreach (Film film in filmovi)
            {
                film.Recenzije = db.Recenzije.Where(r => r.FilmId == film.Id).ToList();
                film.Reziseri = db.ReziseriFilmova.Include(r => r.Umetnik).Where(r => r.FilmId == film.Id).ToList();
                film.Uloge = db.Uloge.Include(r => r.Glumac).Where(r => r.FilmId == film.Id).ToList();
                double sum = 0;
                int counter = 0;
                foreach (Recenzija rec in film.Recenzije)
                {
                    sum += rec.BrojZvezdica;
                    counter++;
                }
                film.ProsecnaOcena = sum / counter;
            }



            dynamic mymodel = new ExpandoObject();
            if (latestRecenzije.Count() == 0) { mymodel.Recenzije = null; }
            else mymodel.Recenzije = latestRecenzije;
            mymodel.Filmovi = filmovi;
            ViewBag.SearchSuggestions = GetSearchSuggestions();
            return View(mymodel);
        }

        [Authorize]
        public IActionResult Interests()
        {
            List<Film> filmovi = db.Filmovi
                         .Include(z => z.Zanrovi)
                         .ToList();
            // Example: Movie movie = movieService.GetMovieById(id);

            foreach (Film film in filmovi)
            {
                film.Recenzije = db.Recenzije.Where(r => r.FilmId == film.Id).ToList();
                film.Reziseri = db.ReziseriFilmova.Include(r => r.Umetnik).Where(r => r.FilmId == film.Id).ToList();
                film.Uloge = db.Uloge.Include(r => r.Glumac).Where(r => r.FilmId == film.Id).ToList();
                double sum = 0;
                int counter = 0;
                foreach (Recenzija rec in film.Recenzije)
                {
                    sum += rec.BrojZvezdica;
                    counter++;
                }
                film.ProsecnaOcena = sum / counter;
            }
            filmovi = filmovi
            .OrderByDescending(film => film.ProsecnaOcena)
            .Take(10)
            .ToList();


            dynamic mymodel = new ExpandoObject();
            mymodel.Filmovi = filmovi;
            return View(mymodel);
        }

        protected List<string> GetSearchSuggestions()
        {
            // Your logic to fetch search suggestions
            // For example:
            var suggestions = new List<string> { "Suggestion 1", "Suggestion 2", "Suggestion 3" };
            return suggestions;
        }

        [HttpPost]
        public IActionResult Done(FormViewModel model)
        {
            // Handle the form data here
            // You can access form values from the 'model' parameter

            PreferencijaZanr pz = db.PreferencijeZanrova
            .Where(k => k.KorisnikId == LoginRegistracijaController.ulogovaniKorisnik.Id && k.Zanr == (ZanrEnum)Enum.Parse(typeof(ZanrEnum), model.FavZanr))
                        .SingleOrDefault();
            pz.VrednostPreferencije = pz.VrednostPreferencije * 1.1;

            PreferencijaRegion preg = db.PreferencijeRegiona
                    .Where(k => k.KorisnikId == LoginRegistracijaController.ulogovaniKorisnik.Id && k.Region == (Region)Enum.Parse(typeof(Region), model.FavRegion))
                    .SingleOrDefault();
            preg.VrednostPreferencije = preg.VrednostPreferencije * 1.1;
            db.SaveChanges();

            List<Film> filmovi = new List<Film>();
            foreach (int i in model.SelectedMovies)
            {
                filmovi.Add(db.Filmovi.Include(z => z.Zanrovi).Where(k => k.Id == i).SingleOrDefault());
                // Example: Movie movie = movieService.GetMovieById(id);
            }
            foreach (Film film in filmovi)
                {
                    film.Recenzije = db.Recenzije.Where(r => r.FilmId == film.Id).ToList();
                    film.Reziseri = db.ReziseriFilmova.Include(r => r.Umetnik).Where(r => r.FilmId == film.Id).ToList();
                    film.Uloge = db.Uloge.Include(r => r.Glumac).Where(r => r.FilmId == film.Id).ToList();

                PromeniPreferencije(film, 100);
                }
            db.SaveChanges();

            return RedirectToAction("LoggedIn", "Home");

            // Perform necessary processing

            // Redirect or return a view as needed
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
                    pgod.VrednostPreferencije * (0.9 + Math.Abs(i) * 0.02 + rating / 100 * (1 - (0.9 + Math.Abs(i) * 0.02)) / 50);

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
        public IActionResult Privacy()
        {
            return View();
        }
     

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}