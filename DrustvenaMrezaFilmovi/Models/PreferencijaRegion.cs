using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DrustvenaMrezaFilmovi.Models
{
    public class PreferencijaRegion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public Region Region { get; set; }
        [Key]
        [ForeignKey("Korisnik")]
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        [Required]
        [DefaultValue(10)]
        public double VrednostPreferencije { get; set; }
    }
}
