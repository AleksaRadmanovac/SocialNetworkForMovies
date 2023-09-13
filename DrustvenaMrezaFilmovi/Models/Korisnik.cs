using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrustvenaMrezaFilmovi.Models
{
    public class Korisnik
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int Godiste { get; set; }
        [Required]
        public Pol Pol { get; set; }
        [Required]
        public Region Region { get; set; }
        [NotMapped]
        public List<Recenzija> OceneFilmova { get; set; }
        [NotMapped]
        public List<Korisnik> Prijatelji { get; set; } = new List<Korisnik>();
        [NotMapped]
        public Umetnik OmiljeniGlumac { get; set; }
        [NotMapped]
        public Umetnik OmiljeniReziser { get; set; }
        [NotMapped]
        public ZanrEnum OmiljeniZanr { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Korisnik korisnik &&
                   Id == korisnik.Id;
        }
    }
}
