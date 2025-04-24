using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Firma.Intranet.Models.CMS
{
    public class Strona
    {
        [Key]
        public int IdStrony { get; set; }

        [Required(ErrorMessage = "Wymagana nazwa strony")]
        [MaxLength(20, ErrorMessage = "Maksymalna długość to 20 znaków")]
        [Display(Name = "Tytul odnosnika")]

        public required string LinkTytul { get; set; }
        [Required(ErrorMessage = "Wymagana  tytul")]
        [MaxLength(20, ErrorMessage = "Maksymalna długość to 20 znaków")]
        [Display(Name = "Tytul Strony")]
        public required string Tytul { get; set; }

        [Display(Name = "Treść")]
        [Column(TypeName = "nvarchar(MAX)")]
        public required string Tres { get; set; }

        [Display(Name = "Pozycja wyswietlania")]
        [Required(ErrorMessage = "Wymagana Pozycja wyswietlania")]
        public int Pozycja { get; set; }
    }
}
