using Firma.Data.Data.CMS;
using Firma.Data.Data.Sklep;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Data.Data
{
    public class FirmaContext : DbContext
    {
        public FirmaContext(DbContextOptions<FirmaContext> options)
            : base(options)
        {
        }

        public DbSet<Aktualnosc> Aktualnosc { get; set; } = default!;
        public DbSet<Rodzaj> Rodzaj { get; set; } = default!;
        public DbSet<Strona> Strona { get; set; } = default!;
        public DbSet<Towar> Towar { get; set; } = default!;
        public DbSet<Recenzja> Recenzja { get; set; } = default!;
        public DbSet<Uzytkownik> Uzytkownik { get; set; } = default!;
        public DbSet<Zamowienie> Zamowienie { get; set; } = default!;
        public DbSet<Baner> Baner { get; set; } = default!;
        public DbSet<ProduktPromocja> ProduktPromocja { get; set; } = default!;
        public DbSet<Promocja> Promocja { get; set; } = default!;
        public DbSet<Kontakt> Kontakt { get; set; } = default!;
        public DbSet<Onas> Onas { get; set; } = default!;
        public DbSet<PolitykaPrywatnosci> PolitykaPrywatnosci { get; set; } = default!;
        public DbSet<Regualmin> Regualmin { get; set; } = default!;
        public DbSet<PrzydatneLinki> PrzydatneLinki { get; set; } = default!;
        


    }
}
