using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Firma.Intranet.Models.Sklep
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
