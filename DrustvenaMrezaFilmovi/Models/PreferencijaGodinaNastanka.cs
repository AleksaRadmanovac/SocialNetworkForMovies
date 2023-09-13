using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DrustvenaMrezaFilmovi.Models
{
    public class PreferencijaGodinaNastanka
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int GodinaNastanka { get; set; }
        [Key]
        [ForeignKey("Korisnik")]
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        [Required]
        [DefaultValue(10)]
        public double VrednostPreferencije { get; set; }

    }
}
