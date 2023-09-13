using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;

namespace DrustvenaMrezaFilmovi.Models
{
    public class Umetnik
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public Pol Pol { get; set; }
        public int Godiste { get; set; }
        public Region Region { get; set; }

        public override string? ToString()
        {
            string s = Ime + " " + Prezime;
            return s;
        }
    }
}