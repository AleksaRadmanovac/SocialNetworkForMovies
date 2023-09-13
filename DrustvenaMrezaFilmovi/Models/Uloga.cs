using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DrustvenaMrezaFilmovi.Models
{
    public class Uloga
    {
        [Key]
        [ForeignKey("Film")]
        public int FilmId { get; set; }
        public Film Film { get; set; }
        [Key]
        [ForeignKey("Glumac")]
        public int GlumacId { get; set; }
        public Umetnik Glumac { get; set; }
        public string ImeUloge { get; set; }
        public bool JeGlavna { get; set; }
    }
}