using System.Diagnostics;
using Firma.Data.Data;
using Firma.Data.Data.CMS;
using Firma.Data.Data.Sklep;
using Firma.PortalWWW.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.PortalWWW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FirmaContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(ILogger<HomeController> logger, FirmaContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
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
            ViewBag.ModelPromocji = (from pp in _context.ProduktPromocja
                     .Include(pp => pp.Towar)
                     .Include(pp => pp.Promocja)
                                     select pp).ToList();
            ViewBag.ModelBigPromocji = (from pp in _context.ProduktPromocja
                        .Include(pp => pp.Towar)
                        .Include(pp => pp.Promocja)
                                        select pp).FirstOrDefault();

            var zamowienia = (from z in _context.Zamowienie
                              join t in _context.Towar on z.Towar.idTowar equals t.idTowar
                              select new { Zamowienie = z, Towar = t }).ToList();

            ViewBag.ModelPopularne = (from z in zamowienia
                                      group z.Zamowienie by z.Towar into g
                                      let liczbaZamowien = g.Sum(x => x.Ilosc)
                                      orderby liczbaZamowien descending
                                      select g.Key).Take(6).ToList();

            ViewBag.ModelDlaCiebie = (
                from towar in _context.Towar
                orderby towar.idTowar descending
                select towar
                ).Take(6).ToList();

            ViewBag.Aktualnosci = (
                from aktualnosc in _context.Aktualnosc
                orderby aktualnosc.Pozycja
                select aktualnosc
            ).ToList();


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
        public async Task<IActionResult> Przeglad(int? id)
        {

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

        
        public async Task<IActionResult> Zamow(
    int idTowaru, int ilosc, string miasto, string ulica, string kodPocztowy,
    string kraj, string telefon, string email, string? uwagi)
        {
            #region Baza Strony
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
            #endregion
            if (!User.Identity.IsAuthenticated)
            {
                // Zbuduj query string z parametrami zamówienia
                var query = $"?idTowaru={idTowaru}" +
             $"&ilosc={ilosc}" +
             $"&miasto={Uri.EscapeDataString(miasto ?? string.Empty)}" +
             $"&ulica={Uri.EscapeDataString(ulica ?? string.Empty)}" +
             $"&kodPocztowy={Uri.EscapeDataString(kodPocztowy ?? string.Empty)}" +
             $"&kraj={Uri.EscapeDataString(kraj ?? string.Empty)}" +
             $"&telefon={Uri.EscapeDataString(telefon ?? string.Empty)}" +
             $"&email={Uri.EscapeDataString(email ?? string.Empty)}" +
             $"&uwagi={Uri.EscapeDataString(uwagi ?? string.Empty)}"; var returnUrl = Url.Action("Zamow", "Home") + query;
                return RedirectToPage("/Account/Login", new { area = "Identity", returnUrl });
            }
            var userId = _userManager.GetUserId(User);
            var uzytkownik = await _context.Uzytkownik
                .FirstOrDefaultAsync(u => u.ApplicationUserId == userId);
            var towar = await _context.Towar.FindAsync(idTowaru);
            if (towar == null)
                return NotFound();

            var zamowienie = new Zamowienie
            {
                DataZamowienia = DateTime.Now,
                IdUzytkownika = uzytkownik.IdUzytkownika,
                CzyZrealizowane = false,
                CzyAnulowane = false,
                SposobPlatnosci = "Karta",
                IdTowaru = towar.idTowar,
                Ilosc = ilosc,
                Miasto = miasto,
                Ulica = ulica,
                KodPocztowy = kodPocztowy,

            };

            _context.Zamowienie.Add(zamowienie);
            await _context.SaveChangesAsync();

            return RedirectToAction("Podsumowanie", new { id = zamowienie.IdZamowienia });
        }

        public async Task<IActionResult> Podsumowanie(int id)
        {
            #region Baza Strony
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
            #endregion
            var zamowienie = await _context.Zamowienie
                .Include(z => z.Towar)
                .FirstOrDefaultAsync(z => z.IdZamowienia == id);

            if (zamowienie == null)
                return NotFound();

            return View(zamowienie);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize]
        public async Task<IActionResult> Konto(int? id)
        {
            // Pobierz ID aktualnie zalogowanego u¿ytkownika (Identity)
            var userId = _userManager.GetUserId(User);

            // Pobierz powi¹zanego u¿ytkownika sklepu (Uzytkownik)
            var uzytkownik = await _context.Uzytkownik
                .FirstOrDefaultAsync(u => u.ApplicationUserId == userId);

            ViewBag.User = uzytkownik;

            ViewBag.ZamowieniaUser = await _context.Zamowienie
                .Where(z => z.IdUzytkownika == uzytkownik.IdUzytkownika)
                .OrderByDescending(z => z.DataZamowienia)
                .Include(z => z.Towar)
                .ToListAsync();

            ViewBag.RecenzjeUser = await _context.Recenzja
                .Where(r => r.IdUzytkownika == uzytkownik.IdUzytkownika)
                .OrderByDescending(r => r.DataDodania)
                .Include(r => r.Towar)
                .ToListAsync();

            var item = await GetStronaWithModelStronyAsync(id);
            return View(item);
        }
    }



    }
