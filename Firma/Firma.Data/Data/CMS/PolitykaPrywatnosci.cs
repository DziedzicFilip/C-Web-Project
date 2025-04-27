using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Firma.Data.Data.CMS
{
    public  class PolitykaPrywatnosci
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Tytul { get; set; }
        [Required]
        public string Tresc { get; set; }
        [Required]
        public DateTime DataUtworzenia { get; set; }
        [Required]
        public bool Aktywna { get; set; }
       
    }
}
