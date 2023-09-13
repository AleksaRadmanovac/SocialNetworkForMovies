using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrustvenaMrezaFilmovi.Models
{
    public class ZanrFilma
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public ZanrEnum Zanr { get; set; }
        [Key]
        [ForeignKey("Film")]
        public int FilmId { get; set; }
        public Film Film { get; set; }
    }
}
