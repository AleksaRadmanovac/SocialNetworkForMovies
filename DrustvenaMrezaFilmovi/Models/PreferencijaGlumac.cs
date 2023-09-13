using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrustvenaMrezaFilmovi.Models
{
    public class PreferencijaGlumac
    {
        [Key]
        [ForeignKey("Korisnik")]
        [Column(Order = 1)]
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        [Key]
        [ForeignKey("Umetnik")]
        [Column(Order = 2)]
        public int UmetnikId { get; set; }
        public Umetnik Umetnik { get; set; }
        [Required]
        [DefaultValue(10)]
        public double VrednostPreferencije { get; set; }
    }
}
