using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Firma.Data.Data.Sklep
{
    public class ApplicationUser : IdentityUser
    {
        public string Imie { get; set; } = string.Empty;

        public string Nazwisko { get; set; } = string.Empty;
        public Uzytkownik? Uzytkownik { get; set; }
        public string TypKonta { get; set; } = "PortalWWW"; 
        public ICollection<Zamowienie> Zamowienia { get; set; } = new List<Zamowienie>();

        public ICollection<Recenzja> Recenzje { get; set; } = new List<Recenzja>();
    }
}
