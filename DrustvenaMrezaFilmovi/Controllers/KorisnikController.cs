using DrustvenaMrezaFilmovi.Data;
using DrustvenaMrezaFilmovi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace DrustvenaMrezaFilmovi.Controllers
{
    public class KorisnikController : Controller
    {
        private readonly ILogger<KorisnikController> _logger;
        private readonly AppDbContext db;

        public KorisnikController(ILogger<KorisnikController> logger, AppDbContext _db)
        {
            _logger = logger;
            db = _db;

        }
        public IActionResult Details(int id)
        {
            var korisnik = db.Korisnici
                         .Where(f => f.Id == id)
                         .SingleOrDefault();

            if (korisnik == null)
            {
                return NotFound(); // Return a 404 Not Found response if the movie is not found.
            }
            List<Prijateljstvo> listaPrijateljstava = db.Prijateljstva
                        .Include(k => k.Korisnik1)
                        .Include(k => k.Korisnik2)
                        .Where(k => (k.KorisnikId1 == korisnik.Id || k.KorisnikId2 == korisnik.Id) && k.Prihvaceno == true)
                        .ToList();
            List<Korisnik> listaPrijatelja = new List<Korisnik>();
            foreach (Prijateljstvo p in listaPrijateljstava)
            {
                if (p.KorisnikId1 == korisnik.Id)
                    listaPrijatelja.Add(p.Korisnik2);
                else listaPrijatelja.Add(p.Korisnik1);
            }
            korisnik.Prijatelji = listaPrijatelja;

            Umetnik omiljeniGlumac;
            //omiljeniGlumac = 
            omiljeniGlumac = (Umetnik)db.PreferencijeGlumaca
    .Where(p => p.KorisnikId == id)
    .OrderByDescending(p => p.VrednostPreferencije)
    .Select(p => p.Umetnik)
    .FirstOrDefault();

            Umetnik omiljeniReziser;
            //omiljeniGlumac = 
            omiljeniReziser = (Umetnik)db.PreferencijeRezisera
    .Where(p => p.KorisnikId == id)
    .OrderByDescending(p => p.VrednostPreferencije)
    .Select(p => p.Umetnik)
    .FirstOrDefault();

            ZanrEnum omiljeniZanr;
            //omiljeniGlumac = 
            omiljeniZanr = (ZanrEnum)db.PreferencijeZanrova
    .Where(p => p.KorisnikId == id)
    .OrderByDescending(p => p.VrednostPreferencije)
    .Select(p => p.Zanr)
    .FirstOrDefault();

            List<Recenzija> listaRecenzija = db.Recenzije.Include(r => r.Korisnik).Include(f => f.Film).Where(r => r.KorisnikId == id).ToList();

            dynamic mymodel = new ExpandoObject();
            if (LoginRegistracijaController.ulogovan) mymodel.Zahtev = DaLiPostojiZahtev(id);
            mymodel.Korisnik = korisnik;
            mymodel.OmiljeniGlumac = omiljeniGlumac;
            mymodel.OmiljeniReziser = omiljeniReziser;
            mymodel.OmiljeniZanr = omiljeniZanr;
            mymodel.Recenzije = listaRecenzija;
            mymodel.Zahtevi = null;

            if (LoginRegistracijaController.ulogovan)
            {
                List<Prijateljstvo> listaZahtevaZaPrijateljstvo = db.Prijateljstva
                    .Include(r => r.Korisnik1)
                    .Include(r => r.Korisnik2)
                    .Where(r => r.KorisnikId2 == id).ToList();
                mymodel.Zahtevi = listaZahtevaZaPrijateljstvo;
            }


            
            
            return View(mymodel); // Return a view to display the movie details.
        }

        [HttpPost]
        public async Task<IActionResult> DodajPrijatelja(Prijateljstvo prijateljstvo)
        {
            try
            {
                Prijateljstvo trenutno = DaLiPostojiZahtev(prijateljstvo.KorisnikId2);
                if (trenutno == null)
                {
                    prijateljstvo.PocetakPrijateljstva = DateTime.Now;
                    db.Prijateljstva.Add(prijateljstvo);
                    db.SaveChanges();
                }
                else 
                {
                    bool pokazatelj = LoginRegistracijaController.ulogovaniKorisnik.Prijatelji.Remove(trenutno.Korisnik1);
                    ObrisiPrijateljstvo(trenutno);
                    
                }
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Prihvati(Prijateljstvo prijateljstvo)
        {
            try
            {
                Prijateljstvo trenutno = DaLiPostojiZahtev(prijateljstvo.KorisnikId1);
                if (trenutno == null)
                {
                    return Json(new { success = false });
                }
                else
                {
                    if (trenutno.KorisnikId2 == LoginRegistracijaController.ulogovaniKorisnik.Id)
                    {
                        trenutno.PocetakPrijateljstva = DateTime.Now;
                        trenutno.Prihvaceno = true;
                        db.SaveChanges();
                        LoginRegistracijaController.ulogovaniKorisnik.Prijatelji.Add(trenutno.Korisnik1);
                        return Json(new { success = true });
                    }
                    else return Json(new { success = false });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        public void ObrisiPrijateljstvo(Prijateljstvo p)
        {
            db.Prijateljstva.Remove(p);

            // Save changes to commit the deletion to the database
            db.SaveChanges();
        }

        public Prijateljstvo DaLiPostojiZahtev(int korisnikId2) 
        {
            Prijateljstvo rez = db.Prijateljstva
                .Include(k => k.Korisnik1)
                .Include(k => k.Korisnik2)
                .Where(r => ((r.KorisnikId1 == LoginRegistracijaController.ulogovaniKorisnik.Id && r.KorisnikId2 == korisnikId2) || 
                (r.KorisnikId1 == korisnikId2 && r.KorisnikId2 == LoginRegistracijaController.ulogovaniKorisnik.Id)))
                .SingleOrDefault();

            return rez;
        }

    }
}
