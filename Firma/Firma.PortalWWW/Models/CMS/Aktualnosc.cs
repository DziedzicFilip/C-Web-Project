﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Firma.PortalWWW.Models.CMS
{
    public class Aktualnosc
    {
        [Key]
        public int IdAktualnosc { get; set; }

        [Required(ErrorMessage = "tytul odnosnika jest wymagany")]
        [MaxLength(20, ErrorMessage = "Maksymalna długość to 20 znaków")]
        [Display(Name = "Tytul odnosnika")]

        public required string LinkTytul { get; set; }
       
        [Required(ErrorMessage = "Wymagana  tytul")]
        [MaxLength(20, ErrorMessage = "Maksymalna długość to 20 znaków")]
        [Display(Name = "Tytul Aktualnosc ")]
        public required string Tytul { get; set; }

        [Display(Name = "Treść")]
        [Column(TypeName = "navchar(MAX)")]
        public required string Tresc { get; set; }

        [Display(Name = "Pozycja wyswietlania")]
        [Required(ErrorMessage = "Wymagana Pozycja wyswietlania")]
        public int Pozycja { get; set; }
    }
}