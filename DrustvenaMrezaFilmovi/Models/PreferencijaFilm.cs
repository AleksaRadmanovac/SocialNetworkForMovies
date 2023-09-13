using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrustvenaMrezaFilmovi.Models
{
    public class PreferencijaFilm
    {
        [Key]
        [ForeignKey("Korisnik")]
        [Column(Order = 1)]
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        [Key]
        [ForeignKey("Umetnik")]
        [Column(Order = 2)]
        public int FilmId { get; set; }
        public Film Film { get; set; }
        [Required]
        [DefaultValue(10)]
        public double VrednostPreferencije { get; set; }

        public void IzracunajPreferenciju(double pregion, double pgodina, List<double> pzanrovi, List<double> pglumci, List<double> preziseri)
        {
            
            double prefzanr;
            if (pzanrovi.Count == 0) prefzanr = 10;
            else prefzanr = 0;

            foreach (double pzanr in pzanrovi)
            { 
                prefzanr += pzanr/pzanrovi.Count;
            }
            double prefreziseri;
            if (preziseri.Count == 0) prefreziseri = 10;
            else prefreziseri = 0;

            foreach (double preziser in preziseri)
            {
                prefreziseri += preziser / preziseri.Count;
            }
            double prefglumci;
            if (pglumci.Count == 0) prefglumci = 10;
            else prefglumci = 0;

            foreach (double pglumac in pglumci)
            {
                prefglumci += pglumac / pglumci.Count;
            }
            VrednostPreferencije = pregion * 0.1 + pgodina * 0.15 + prefzanr*0.25 + prefreziseri*0.25 + prefglumci*0.25;
        }
    }
}
