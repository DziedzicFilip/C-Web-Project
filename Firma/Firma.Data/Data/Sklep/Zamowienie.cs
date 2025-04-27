using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Data.Data.Sklep
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

        [Required]
        public int IdTowaru { get; set; }

        [ForeignKey(nameof(IdTowaru))]
        public Towar? Towar { get; set; }

        [Required(ErrorMessage = "Wymagana ilość")]
        public int Ilosc { get; set; }
    }
}
