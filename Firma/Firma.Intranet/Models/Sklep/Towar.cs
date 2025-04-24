using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Firma.Intranet.Models.Sklep
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

        [Required(ErrorMessage = "Wymagana ilosc")]
        [Display(Name = "Ilosc")]
        public required int Ilosc { get; set; } = 0;


        [ForeignKey("Rodzaj")]
        public int idRodzaju { get; set; }
        public Rodzaj? Rodzaj { get; set; }

        public ICollection<Recenzja> Recenzje { get; set; } = new List<Recenzja>();
        public ICollection<ZamowienieTowar> ZamowieniaTowary { get; set; } = new List<ZamowienieTowar>();



    }
}
