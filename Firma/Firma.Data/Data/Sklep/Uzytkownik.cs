using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Data.Data.Sklep
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
        [MaxLength(20, ErrorMessage = "Login może mieć maksymalnie 20 znaków")]
        public string? Login { get; set; } 
        [MaxLength(20, ErrorMessage = "Hasło może mieć maksymalnie 20 znaków")]
        public string? Haslo { get; set; }
        public string? ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser? ApplicationUser { get; set; }
        public ICollection<Zamowienie> Zamowienia { get; set; } = new List<Zamowienie>();
        public ICollection<Recenzja> Recenzje { get; set; } = new List<Recenzja>();
    }
}
