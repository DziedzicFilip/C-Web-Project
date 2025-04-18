using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Firma.PortalWWW.Models.Sklep
{
    public class Towar
    {
        [Key]
        public int idTowar { get; set; }
        [Required(ErrorMessage = "Wymagana nazwa towaru")]
        [MaxLength(20, ErrorMessage = "Maksymalna długość to 20 znaków")]
        public required string Nazwa { get; set; }

        [Required(ErrorMessage = "Wymagany Kod")]
        public required string Kod { get; set; }
        [Required(ErrorMessage = "Wymagana Cena")]
        [Column(TypeName = "money")]

        public required decimal Cena { get; set; }
        [Required(ErrorMessage = "Wymagany link do zdjecia")]
        [Display(Name = "Link do zdjecia")]
        public required string FotoUrl { get; set; }
        public required string Opis { get; set; } = string.Empty;




    }
}
