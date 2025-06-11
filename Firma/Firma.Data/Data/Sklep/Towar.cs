using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Firma.Data.Data.Sklep
{
    public class Towar
    {
        [Key]
        public int idTowar { get; set; }

        [Required(ErrorMessage = "Wymagana nazwa towaru")]
        [MaxLength(20, ErrorMessage = "Maksymalna długość to 20 znaków")]
        public required string Nazwa { get; set; }

        [Required(ErrorMessage = "Wymagany Kod")]
        public required string Kod { get; set; }

        [Required(ErrorMessage = "Wymagana Cena")]
        [Range(0, double.MaxValue, ErrorMessage = "Cena musi być większa niż 0")]
        [Column(TypeName = "money")]
        public required decimal Cena { get; set; }

        [Display(Name = "Link do zdjęcia")]
        public string? FotoUrl { get; set; }

        public required string Opis { get; set; } = string.Empty;

        [Required(ErrorMessage = "Wymagana ilość")]
        [Display(Name = "Ilość")]
        public required int Ilosc { get; set; } 

        [ForeignKey("Rodzaj")]
        public int idRodzaju { get; set; }
        public Rodzaj? Rodzaj { get; set; }

        public ICollection<Recenzja> Recenzje { get; set; } = new List<Recenzja>();
        public ICollection<ProduktPromocja>? ProduktyPromocji { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Waga { get; set; }

        [Display(Name = "Wymiary (DłxSzxWy)")]
        [MaxLength(50, ErrorMessage = "Maksymalna długość opisu wymiarów to 50 znaków")]
        public string? Wymiary { get; set; }

        [MaxLength(30, ErrorMessage = "Maksymalna długość koloru to 30 znaków")]
        public string? Kolor { get; set; }

        [MaxLength(50, ErrorMessage = "Maksymalna długość materiału to 50 znaków")]
        public string? Material { get; set; }

        [MaxLength(50, ErrorMessage = "Maksymalna długość producenta to 50 znaków")]
        public string? Producent { get; set; }

        [MaxLength(50, ErrorMessage = "Maksymalna długość kraju produkcji to 50 znaków")]
        public string? KrajProdukcji { get; set; }

        [Display(Name = "Gwarancja (miesiące)")]
        [Range(0, 120, ErrorMessage = "Gwarancja musi być pomiędzy 0 a 120 miesięcy")]
        public int? GwarancjaMiesiace { get; set; }

        [MaxLength(50, ErrorMessage = "Maksymalna długość modelu to 50 znaków")]
        public string? Model { get; set; }

       
        [Display(Name = "Stan produktu")]
        public StanProduktow? Stan { get; set; }
    }

    public enum StanProduktow
    {
        Nowy,
        Uzywany,
        Odnowiony
    }
}
