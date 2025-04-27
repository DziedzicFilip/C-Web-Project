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
            var item = await GetStronaWithModelStronyAsync(id);
            return View(item);
        }

        public async Task<IActionResult> Privacy(int? id)
        {
            var item = await GetStronaWithModelStronyAsync(id);
            return View(item);
        }
        public async Task<IActionResult> Przeglad(int? id) {
            var item = await GetStronaWithModelStronyAsync(id);
            return View(item);
        }
        public async Task<IActionResult> Szczegoly(int? id)
        {
            var item = await GetStronaWithModelStronyAsync(id);
            return View(item);
        }
        public async Task<IActionResult> Kontakt(int? id)
        {
            var item = await GetStronaWithModelStronyAsync(id);
            return View(item); ;
        }
        public async Task<IActionResult> Regulamin(int? id)
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
