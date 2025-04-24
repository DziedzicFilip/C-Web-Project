using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Firma.Intranet.Models.Sklep
{
    public class ZamowienieTowar
    {
        [Key]
        public int IdZamowienieTowar { get; set; }
        
        [Required]
        public int IdZamowienia { get; set; }

        [ForeignKey(nameof(IdZamowienia))]
        public Zamowienie? Zamowienie { get; set; }

        [Required]
        public int IdTowaru { get; set; }

        [ForeignKey(nameof(IdTowaru))]
        public Towar? Towar { get; set; }

        [Required(ErrorMessage = "Wymagana ilość")]
        public int Ilosc { get; set; }
    }

}
