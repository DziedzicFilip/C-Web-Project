using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Firma.Intranet.Models.Sklep
{
    public class Promocja
    {
        [Key]
        public int IdPromocji { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nazwa { get; set; } = string.Empty;

        [Range(0, 1, ErrorMessage = "Rabat musi być między 0 a 1 (np. 0.2 = 20%")]
        public decimal Rabat { get; set; }

        [Display(Name = "Data rozpoczęcia")]
        public DateTime DataRozpoczecia { get; set; }

        [Display(Name = "Data zakończenia")]
        public DateTime DataZakonczenia { get; set; }

        public bool CzyAktywna { get; set; }

        public ICollection<ProduktPromocja>? ProduktyPromocji { get; set; }
    }
}
