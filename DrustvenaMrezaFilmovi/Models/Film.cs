using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrustvenaMrezaFilmovi.Models
{
    public class Film
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Naziv { get; set; }
        [NotMapped]
        public List<ZanrFilma> Zanrovi { get; set; }
        public int GodinaNastanka { get; set; }
        public Region Region { get; set; }
        [NotMapped]
        public List<Recenzija> Recenzije { get; set; }
        [NotMapped]
        public double ProsecnaOcena { get; set; }
        [NotMapped]
        public List<Uloga> Uloge { get; set; }
        [NotMapped]
        public List<ReziserFilma> Reziseri { get; set; } 
    }
}