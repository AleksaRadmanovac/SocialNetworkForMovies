using DrustvenaMrezaFilmovi.Data;
using DrustvenaMrezaFilmovi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.CodeAnalysis.CSharp;
using System.Dynamic;

namespace DrustvenaMrezaFilmovi.Controllers
{
    public class LoginRegistracijaController : Controller
    {
        private readonly ILogger<LoginRegistracijaController> _logger;
        private readonly AppDbContext db;
        public static Korisnik ulogovaniKorisnik = null;
        public static bool ulogovan = false;
        /* private readonly UserManager<Korisnik> _userManager;
         private readonly SignInManager<Korisnik> _signInManager;
         , UserManager<Korisnik> userManager, SignInManager<Korisnik> signInManager*/

        public LoginRegistracijaController(ILogger<LoginRegistracijaController> logger, AppDbContext _db)
        {
            _logger = logger;
            db = _db;
           /* _userManager = userManager;
            _signInManager = signInManager;*/
        }

        [HttpPost]
        public async Task<IActionResult> Register(Korisnik korisnik)
        {
            Console.WriteLine(123);
            if (korisnik.Username != null && korisnik.Username != "")
            {
                List<Korisnik> usersWithSameUsername = db.Korisnici
            .Where(u => u.Username == korisnik.Username)
            .ToList();
                if (usersWithSameUsername.Count == 0)
                {          
                    db.Korisnici.Add(korisnik);
                    db.SaveChanges();
                    ulogovaniKorisnik = korisnik;
                    ulogovan = true;
                    List<Claim> claims = new List<Claim>() {
                        new Claim(ClaimTypes.NameIdentifier, korisnik.Username)

                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = false
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), properties);
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false }); ;
        }

        public async Task<IActionResult> LogOut()
        {
            ulogovan = false;
            ulogovaniKorisnik = null;
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Korisnik korisnik)
        {
            Console.WriteLine(123);
            if (korisnik.Username != null && korisnik.Username != "")
            {
                List<Korisnik> ulogovani = (List<Korisnik>)db.Korisnici
            .Where(u => u.Username == korisnik.Username)
            .Where(u => u.Password == korisnik.Password)
            .Take(1).ToList();

                if (ulogovani.Count != 0 && ulogovani[0] != null)
                {
                    List<Prijateljstvo> listaPrijateljstava = db.Prijateljstva
                        .Include(k => k.Korisnik1)
                        .Include(k => k.Korisnik2)
                        .Where(k => (k.KorisnikId1 == ulogovani[0].Id || k.KorisnikId2 == ulogovani[0].Id) && k.Prihvaceno == true)
                        .ToList();
                    List<Korisnik> listaPrijatelja = new List<Korisnik>();
                    foreach (Prijateljstvo p in listaPrijateljstava)
                    {
                        if (p.KorisnikId1 == ulogovani[0].Id)
                            listaPrijatelja.Add(p.Korisnik2);
                        else listaPrijatelja.Add(p.Korisnik1);
                    }
                    ulogovani[0].Prijatelji = listaPrijatelja;
                    ulogovan = true;
                    ulogovaniKorisnik = ulogovani[0];
                    List<Claim> claims = new List<Claim>() {
                        new Claim(ClaimTypes.NameIdentifier, korisnik.Username)

                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = false
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), properties);

                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });

        }



    }
}
