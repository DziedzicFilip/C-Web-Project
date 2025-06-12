using Firma.Data.Data.Sklep;
using Firma.Data.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
public class StatystykiModel : PageModel
{
    private readonly FirmaContext _context;

    public StatystykiModel(FirmaContext context)
    {
        _context = context;
    }

    public decimal SprzedazDzisiaj { get; set; }
    public int OdwiedzinyDzisiaj { get; set; }
    public int NowiKlienciDzisiaj { get; set; }
    public int ProduktySprzedaneDzisiaj { get; set; }
    public List<Zamowienie> Zamowienia { get; set; } = [];

    public void OnGet()
    {
        var dzis = DateTime.Today;
        Zamowienia = _context.Zamowienie
            .Include(z => z.Towar)
            .Where(z => z.DataZamowienia.Date == dzis)
            .ToList();

        SprzedazDzisiaj = Zamowienia.Sum(z => z.Towar != null ? z.Towar.Cena * z.Ilosc : 0);
        ProduktySprzedaneDzisiaj = Zamowienia.Sum(z => z.Ilosc);
        NowiKlienciDzisiaj = Zamowienia.Select(z => z.Uzytkownik?.IdUzytkownika).Distinct().Count();
        OdwiedzinyDzisiaj = 0; // jeśli nie masz takiej tabeli, zostaw 0
    }
}
