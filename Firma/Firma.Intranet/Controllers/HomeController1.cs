using Firma.Data.Data;
using Firma.Data.Data.Sklep;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Firma.Intranet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatystykiController : ControllerBase
    {
        private readonly FirmaContext _context;

        public StatystykiController(FirmaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatystyki(
            DateTime? dataOd = null,
            DateTime? dataDo = null,
            int? idRodzaju = null)
        {
            var zamowienia = _context.Zamowienie
                .Include(z => z.Towar)
                .ThenInclude(t => t.Rodzaj)
                .AsQueryable();

            if (dataOd.HasValue)
                zamowienia = zamowienia.Where(z => z.DataZamowienia >= dataOd.Value);
            if (dataDo.HasValue)
                zamowienia = zamowienia.Where(z => z.DataZamowienia <= dataDo.Value);
            if (idRodzaju.HasValue)
                zamowienia = zamowienia.Where(z => z.Towar != null && z.Towar.idRodzaju == idRodzaju.Value);

            var zamowieniaList = await zamowienia.ToListAsync();

            // Statystyki ogólne
            var iloscZamowien = zamowieniaList.Count;
            var sumaProduktow = zamowieniaList.Sum(z => z.Ilosc);
            var sumaPrzychodu = zamowieniaList.Sum(z => z.Towar != null ? z.Towar.Cena * z.Ilosc : 0);

            // Statystyka wg rodzaju towaru
            var wgRodzaju = zamowieniaList
                .GroupBy(z => z.Towar?.Rodzaj?.Nazwa ?? "Brak")
                .Select(g => new { Rodzaj = g.Key, Ilosc = g.Sum(z => z.Ilosc) })
                .ToList();

            // Statystyka wg regionu (miasto/kraj)
            var wgRegionu = zamowieniaList
                .GroupBy(z => z.Miasto)
                .Select(g => new { Miasto = g.Key, Ilosc = g.Count() })
                .OrderByDescending(x => x.Ilosc)
                .ToList();

            // Statystyka wg daty (np. dzień)
            var wgDnia = zamowieniaList
                .GroupBy(z => z.DataZamowienia.Date)
                .Select(g => new { Data = g.Key, Ilosc = g.Sum(z => z.Ilosc), Przychod = g.Sum(z => z.Towar != null ? z.Towar.Cena * z.Ilosc : 0) })
                .OrderBy(x => x.Data)
                .ToList();

            return Ok(new
            {
                iloscZamowien,
                sumaProduktow,
                sumaPrzychodu,
                wgRodzaju,
                wgRegionu,
                wgDnia
            });
        }
    }
}
