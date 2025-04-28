using System.Diagnostics;
using AspNetCoreGeneratedDocument;
using Firma.Data.Data;
using Firma.Data.Data.CMS;
using Firma.Data.Data.Sklep;
using Firma.PortalWWW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.PortalWWW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FirmaContext _context;
        public HomeController(ILogger<HomeController> logger,FirmaContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        private async Task<Strona?> GetStronaWithModelStronyAsync(int? id)
        {
            
            ViewBag.ModelStrony = (
                from strona in _context.Strona
                orderby strona.Pozycja
                select strona
            ).ToList();

            ViewBag.ModelBaner = (
                from baner in _context.Baner
                select baner
            ).ToList();
           
            ViewBag.ModelLinki = (
                from linki in _context.PrzydatneLinki
                select linki
            ).ToList();
            ViewBag.ModelOnas = (
                from onas in _context.Onas
                select onas
            ).ToList();

            ViewBag.ModelKontakt = (
                from kontakt in _context.Kontakt
                select kontakt
            ).ToList();

            if (id == null)
            {
                id = 1;
            }

            
            return await _context.Strona.FindAsync(id);
        }


        public async Task<IActionResult> Index(int? id)
        {
            ViewBag.ModelPromocji = _context.ProduktPromocja
            .Include(pp => pp.Towar)  // pozwlaa na dolaczenie dancy z atabeli Towar
            .Include(pp => pp.Promocja) // pozwlaa na do³aczenie danych z tabeli Promocja
            .ToList();
                    ViewBag.ModelBigPromocji = _context.ProduktPromocja
           .Include(pp => pp.Towar)  
           .Include(pp => pp.Promocja)
           .FirstOrDefault();

            ViewBag.ModelPopularne = _context.Zamowienie
             .GroupBy(z => z.Towar) // Grupuj zamówienia po Towar
             .Select(g => new
             {
                 Produkt = g.Key, // Klucz grupy (Towar)
                 LiczbaZamowien = g.Sum(z => z.Ilosc) // Suma iloœci zamówionych produktów
             })
             .OrderByDescending(x => x.LiczbaZamowien) 
             .Take(6) 
             .Select(x => x.Produkt) // Wybierz same produkty
             .ToList();

            ViewBag.ModelDlaCiebie = (
                from towar in _context.Towar
                orderby towar.idTowar descending
                select towar 
                ).Take(6).ToList();



            var item = await GetStronaWithModelStronyAsync(id);
            return View(item);
        }

        public async Task<IActionResult> Privacy(int? id)
        {
            ViewBag.ModelPolityka = (
                from polityka in _context.PolitykaPrywatnosci
                select polityka
            ).ToList();
            var item = await GetStronaWithModelStronyAsync(id);
            return View(item);
        }
        public async Task<IActionResult> Przeglad(int? id) {

            ViewBag.ModelProdukty = (
                from towar in _context.Towar
                orderby towar.idTowar descending
                select towar
            ).ToList();
            var item = await GetStronaWithModelStronyAsync(id);
            return View(item);


        }
        public async Task<IActionResult> Szczegoly(int? id_towaru)
        {
            ViewBag.ModelStrony = (
                 from strona in _context.Strona
                 orderby strona.Pozycja
                 select strona
             ).ToList();

            ViewBag.ModelBaner = (
                from baner in _context.Baner
                select baner
            ).ToList();

            ViewBag.ModelLinki = (
                from linki in _context.PrzydatneLinki
                select linki
            ).ToList();
            ViewBag.ModelOnas = (
                from onas in _context.Onas
                select onas
            ).ToList();

            ViewBag.ModelKontakt = (
                from kontakt in _context.Kontakt
                select kontakt
            ).ToList();


            if (id_towaru == null)
            {
                return NotFound(); 
            }

            var towar = await _context.Towar.FindAsync(id_towaru);
            if (towar == null)
            {
                return NotFound(); 
            }

            return View(towar); 
        }

        public async Task<IActionResult> Kontakt(int? id)
        {
                        ViewBag.ModelKontakt = (
                from kontakt in _context.Kontakt
                
                select kontakt
            ).ToList();

            var item = await GetStronaWithModelStronyAsync(id);
            return View(item); ;
        }
        public async Task<IActionResult> Regulamin(int? id)
        {
            ViewBag.ModelRegulamin = (
                from regulamin in _context.Regualmin
                select regulamin
            ).ToList();
            var item = await GetStronaWithModelStronyAsync(id);
            return View(item);
        }

        public async Task<IActionResult> Konto(int? id)
        {
            var item = await GetStronaWithModelStronyAsync(id);
            return View(item);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
