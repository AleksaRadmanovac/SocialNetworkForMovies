using DrustvenaMrezaFilmovi.Models;

namespace DrustvenaMrezaFilmovi.Controllers
{
    public class SearchResultHelp
    {
        public List<Film> Filmovi { get; set; }
         = new List<Film>();
        public List<Korisnik> Korisnici { get; set; }
         = new List<Korisnik>();
        public List<Umetnik> Umetnici { get; set; }
         = new List<Umetnik>();
    }
}
