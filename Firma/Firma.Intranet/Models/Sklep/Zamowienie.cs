using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Firma.Intranet.Models.Sklep
{
    public class Zamowienie
    {
        [Key]
        public int IdZamowienia { get; set; }

        [Required]
        [Display(Name = "Data zamówienia")]
        public DateTime DataZamowienia { get; set; }

        public int IdUzytkownika { get; set; }

        [ForeignKey(nameof(IdUzytkownika))]
        public Uzytkownik? Uzytkownik { get; set; }

        // Adres dostawy
        [Required(ErrorMessage = "Wymagana ulica")]
        public string Ulica { get; set; } = string.Empty;

        [Required(ErrorMessage = "Wymagane miasto")]
        public string Miasto { get; set; } = string.Empty;

        [Required(ErrorMessage = "Wymagany kod pocztowy")]
        public string KodPocztowy { get; set; } = string.Empty;

        [Required(ErrorMessage = "Wymagany kraj")]
        public string Kraj { get; set; } = string.Empty;

        [Display(Name = "Czy zrealizowane")]
        public bool CzyZrealizowane { get; set; }

        [Display(Name = "Czy anulowane")]
        public bool CzyAnulowane { get; set; }

        [Display(Name = "Spobosb Platności")]
        public string SposobPlatnosci { get; set; } = string.Empty;

        // Nowe właściwości
        [Required]
        public int IdTowaru { get; set; }

        [ForeignKey(nameof(IdTowaru))]
        public Towar? Towar { get; set; }

        [Required(ErrorMessage = "Wymagana ilość")]
        public int Ilosc { get; set; }
    }
}
