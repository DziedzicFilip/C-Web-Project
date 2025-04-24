using System.ComponentModel.DataAnnotations;

namespace Firma.Intranet.Models.Sklep
{
    public class Rodzaj
    {
        [Key]
        public int IdRodzaju { get; set; }

        [Required(ErrorMessage = "Wymagana nazwa rodzaju")]
        [MaxLength(20, ErrorMessage = "Maksymalna długość to 20 znaków")]
        public required string Nazwa { get; set; }

        public ICollection<Towar>? Towary { get; } = new List<Towar>();


    }
}
