using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Data.Data.Sklep
{
    public class Recenzja
    {
        [Key]
        public int IdRecenzji { get; set; }

        [Range(1, 5, ErrorMessage = "Ocena musi być między 1 a 5")]
        public int Ocena { get; set; }

        [MaxLength(500)]
        public string Komentarz { get; set; } = string.Empty;

        [Display(Name = "Data dodania")]
        public DateTime DataDodania { get; set; }

        public int IdTowaru { get; set; }

        [ForeignKey(nameof(IdTowaru))]
        public Towar? Towar { get; set; }

        public int IdUzytkownika { get; set; }

        [ForeignKey(nameof(IdUzytkownika))]
        public Uzytkownik? Uzytkownik { get; set; }
    }
}
