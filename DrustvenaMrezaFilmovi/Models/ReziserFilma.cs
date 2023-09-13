using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DrustvenaMrezaFilmovi.Models
{
    public class ReziserFilma
    {
        [Key]
        [ForeignKey("Film")]
        public int FilmId { get; set; }
        public Film Film { get; set; }
        [Key]
        [ForeignKey("Umetnik")]
        public int UmetnikId { get; set; }
        public Umetnik Umetnik { get; set; }
    }
}
