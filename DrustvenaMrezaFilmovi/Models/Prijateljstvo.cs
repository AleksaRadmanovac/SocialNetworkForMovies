using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DrustvenaMrezaFilmovi.Models
{
    public class Prijateljstvo
    {
        [Key]
        [ForeignKey("Korisnik1")]
        public int KorisnikId1 { get; set; }
        public Korisnik Korisnik1 { get; set; }
        [Key]
        [ForeignKey("Korisnik2")]
        public int KorisnikId2 { get; set; }
        public Korisnik Korisnik2 { get; set; }
        public DateTime PocetakPrijateljstva { get; set; }
        public double Uticaj1 { get; set; } = 10;
        public double Uticaj2 { get; set; } = 10;
        public bool Prihvaceno { get; set; } = false;
    }
}
