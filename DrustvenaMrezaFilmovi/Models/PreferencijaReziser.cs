using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DrustvenaMrezaFilmovi.Models
{
    public class PreferencijaReziser
    {
        [Key]
        [ForeignKey("Korisnik")]
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        [Key]
        [ForeignKey("Umetnik")]
        public int UmetnikId { get; set; }
        public Umetnik Umetnik { get; set; }
        [Required]
        [DefaultValue(10)]
        public double VrednostPreferencije { get; set; }
    }
}
