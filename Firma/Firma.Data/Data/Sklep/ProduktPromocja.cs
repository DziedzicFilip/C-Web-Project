using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Data.Data.Sklep
{
    public class ProduktPromocja
    {
        [Key]
        public int IdProduktyPromocji { get; set; }

        [Required]
        public int IdTowaru { get; set; }

        [ForeignKey(nameof(IdTowaru))]
        public Towar? Towar { get; set; }

        [Required]
        public int IdPromocji { get; set; }

        [ForeignKey(nameof(IdPromocji))]
        public Promocja? Promocja { get; set; }
    }
}
