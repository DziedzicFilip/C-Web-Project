using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Data.Data.Sklep
{
    public class Promocja
    {
        [Key]
        public int IdPromocji { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nazwa { get; set; } = string.Empty;


        public double Rabat { get; set; }

        [Display(Name = "Data rozpoczęcia")]
        public DateTime DataRozpoczecia { get; set; }

        [Display(Name = "Data zakończenia")]
        public DateTime DataZakonczenia { get; set; }

        public bool CzyAktywna { get; set; }

        public ICollection<ProduktPromocja>? ProduktyPromocji { get; set; }
    }
}
