using System.ComponentModel.DataAnnotations;

namespace Firma.Intranet.Models.Sklep
{
    public class Uzytkownik
    {
        [Key]
        public int IdUzytkownika { get; set; }

        [Required(ErrorMessage = "Wymagane imię")]
        public string Imie { get; set; } = string.Empty;

        [Required(ErrorMessage = "Wymagane nazwisko")]
        public string Nazwisko { get; set; } = string.Empty;

        [Required(ErrorMessage = "Wymagany e-mail")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public ICollection<Zamowienie> Zamowienia { get; set; } = new List<Zamowienie>();
        public ICollection<Recenzja> Recenzje { get; set; } = new List<Recenzja>();
    }
}
