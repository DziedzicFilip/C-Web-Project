﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Data.Data.Sklep
{
    public class Rodzaj
    {
        [Key]
        public int IdRodzaju { get; set; }

        [Required(ErrorMessage = "Wymagana nazwa rodzaju")]
        [MaxLength(20, ErrorMessage = "Maksymalna długość to 20 znaków")]
        public required string Nazwa { get; set; }

        public ICollection<Towar>? Towary { get; } = new List<Towar>();


    }
}
