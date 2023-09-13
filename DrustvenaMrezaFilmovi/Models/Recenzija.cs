using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrustvenaMrezaFilmovi.Models
{
    public class Recenzija
    {
        [Key]
        [ForeignKey("Film")]
        public int FilmId { get; set; }
        public Film Film { get; set; }
        [Key]
        [ForeignKey("Korisnik")]
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        [Required]
        public int BrojZvezdica { get; set; }
        [StringLength(300)]
        public string Komentar { get; set; }
        public DateTime VremeNastanka { get; set; }

    }
}