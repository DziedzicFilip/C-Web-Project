using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Firma.Data.Data.CMS
{
    public  class Kontakt
    {
        [Key]
        public int IdKonakt { get; set; }
        [Required]
        public string? Biuro { get; set; }
        [Required]
        public string? Adres { get; set; }
        [Required]
        public string? KodPocztowy { get; set; }
        [Required]
        public string? Miasto { get; set; }
        [Required]
        public string? Telefon { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? GodzinyPracy { get; set; }





    }
}
