using Firma.Data.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

public class StatystykiModel : PageModel
{
    private readonly FirmaContext _context;

    public decimal SprzedazDzisiaj { get; set; }
    public int ProduktySprzedaneDzisiaj { get; set; }
    public string NajczesciejTowar { get; set; } = "-";
    public int NajczesciejTowarIlosc { get; set; }
    public string NajrzadziejTowar { get; set; } = "-";
    public int NajrzadziejTowarIlosc { get; set; }
    public string NajczesciejMiasto { get; set; } = "-";
    public string NajczesciejKod { get; set; } = "-";
    public int NajczesciejMiastoIlosc { get; set; }
    public string NajrzadziejMiasto { get; set; } = "-";
    public string NajrzadziejKod { get; set; } = "-";
    public int NajrzadziejMiastoIlosc { get; set; }
    public List<SprzedazSzczegolyDto> SprzedazSzczegoly { get; set; } = new();

    public StatystykiModel(FirmaContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync()
    {
        var dzisiaj = DateTime.Today;
        var trzydziesciDni = dzisiaj.AddDays(-30);

        // Sprzedaż dzisiaj
        SprzedazDzisiaj = await _context.Zamowienie
            .Where(z => z.DataZamowienia.Date == dzisiaj && !z.CzyAnulowane && z.Towar != null)
            .SumAsync(z => z.Towar.Cena * z.Ilosc);

        // Produkty sprzedane dzisiaj
        ProduktySprzedaneDzisiaj = await _context.Zamowienie
            .Where(z => z.DataZamowienia.Date == dzisiaj && !z.CzyAnulowane)
            .SumAsync(z => z.Ilosc);

        // Najczęściej zamawiany towar w ostatnich 30 dniach
        var najczesciej = await _context.Zamowienie
            .Where(z => z.DataZamowienia >= trzydziesciDni && !z.CzyAnulowane && z.Towar != null)
            .GroupBy(z => z.Towar.Nazwa)
            .Select(g => new { Nazwa = g.Key, Ilosc = g.Sum(z => z.Ilosc) })
            .OrderByDescending(g => g.Ilosc)
            .FirstOrDefaultAsync();

        if (najczesciej != null)
        {
            NajczesciejTowar = najczesciej.Nazwa;
            NajczesciejTowarIlosc = najczesciej.Ilosc;
        }

        // Najrzadziej zamawiany towar w ostatnich 30 dniach
        var najrzadziej = await _context.Zamowienie
            .Where(z => z.DataZamowienia >= trzydziesciDni && !z.CzyAnulowane && z.Towar != null)
            .GroupBy(z => z.Towar.Nazwa)
            .Select(g => new { Nazwa = g.Key, Ilosc = g.Sum(z => z.Ilosc) })
            .OrderBy(g => g.Ilosc)
            .FirstOrDefaultAsync();

        if (najrzadziej != null)
        {
            NajrzadziejTowar = najrzadziej.Nazwa;
            NajrzadziejTowarIlosc = najrzadziej.Ilosc;
        }

        // Najczęściej zamawiane miasto i kod pocztowy
        var najczMiasto = await _context.Zamowienie
            .Where(z => !z.CzyAnulowane)
            .GroupBy(z => new { z.Miasto, z.KodPocztowy })
            .Select(g => new { g.Key.Miasto, g.Key.KodPocztowy, Liczba = g.Count() })
            .OrderByDescending(g => g.Liczba)
            .FirstOrDefaultAsync();

        if (najczMiasto != null)
        {
            NajczesciejMiasto = najczMiasto.Miasto;
            NajczesciejKod = najczMiasto.KodPocztowy;
            NajczesciejMiastoIlosc = najczMiasto.Liczba;
        }

        // Najrzadziej zamawiane miasto i kod pocztowy
        var najrzMiasto = await _context.Zamowienie
            .Where(z => !z.CzyAnulowane)
            .GroupBy(z => new { z.Miasto, z.KodPocztowy })
            .Select(g => new { g.Key.Miasto, g.Key.KodPocztowy, Liczba = g.Count() })
            .OrderBy(g => g.Liczba)
            .FirstOrDefaultAsync();

        if (najrzMiasto != null)
        {
            NajrzadziejMiasto = najrzMiasto.Miasto;
            NajrzadziejKod = najrzMiasto.KodPocztowy;
            NajrzadziejMiastoIlosc = najrzMiasto.Liczba;
        }

        // Szczegóły sprzedaży (ostatnie 10 zamówień)
        SprzedazSzczegoly = await _context.Zamowienie
            .Where(z => !z.CzyAnulowane && z.Towar != null)
            .OrderByDescending(z => z.DataZamowienia)
            .Take(10)
            .Select(z => new SprzedazSzczegolyDto
            {
                Produkt = z.Towar.Nazwa,
                Ilosc = z.Ilosc,
                Przychod = z.Towar.Cena * z.Ilosc,
                DataSprzedazy = z.DataZamowienia
            })
            .ToListAsync();
    }

    public class SprzedazSzczegolyDto
    {
        public string Produkt { get; set; } = "";
        public int Ilosc { get; set; }
        public decimal Przychod { get; set; }
        public DateTime DataSprzedazy { get; set; }
    }
}
