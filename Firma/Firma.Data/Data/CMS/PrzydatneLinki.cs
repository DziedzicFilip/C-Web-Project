using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Data.Data.CMS
{
    public  class PrzydatneLinki
    {
        [Key]
        public int IdPrzydatneLinki { get; set; }
        [Required]
        public string Tytul { get; set; }
        [Required]
        public string Link { get; set; }
    }
}
