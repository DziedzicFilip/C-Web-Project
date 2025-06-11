using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Data.Data.CMS
{
    public class Baner
    {
        [Key]
        public int IdBanera { get; set; }

        [Required]
        [MaxLength(200)]
        public string Tytul { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Zawartosc { get; set; } = string.Empty;

        
        
        public string UrlObrazka { get; set; } = string.Empty;

        [Display(Name = "Data początkowa")]
        public DateTime DataPoczatkowa { get; set; }

        [Display(Name = "Data zakończenia")]
        public DateTime DataZakonczenia { get; set; }

        public bool CzyAktywny { get; set; }
    }
}
