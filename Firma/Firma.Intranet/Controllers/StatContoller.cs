using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firma.Data.Data;
using Firma.Data.Data.Sklep;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Drawing;
using QuestPDF.Previewer;

namespace Firma.Intranet.Controllers
{
    [Authorize]
    public class StatController : Controller
    {
        private readonly FirmaContext _context;

        public StatController(FirmaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dzisiaj = DateTime.Today;
            var trzydziesciDni = dzisiaj.AddDays(-30);

            // Sprzedaż dzisiaj
            var sprzedazDzisiaj = await _context.Zamowienie
                .Where(z => z.DataZamowienia.Date == dzisiaj && !z.CzyAnulowane && z.CzyZrealizowane)
                .SumAsync(z => z.Towar != null ? z.Towar.Cena * z.Ilosc : 0);

            // Produkty sprzedane dzisiaj
            var produktySprzedaneDzisiaj = await _context.Zamowienie
                .Where(z => z.DataZamowienia.Date == dzisiaj && !z.CzyAnulowane && z.CzyZrealizowane)
                .SumAsync(z => z.Ilosc);

            // Najczęściej zamawiany towar w ostatnich 30 dniach
            var najczesciejZamawiany = await _context.Zamowienie
                .Where(z => z.DataZamowienia >= trzydziesciDni && !z.CzyAnulowane && z.CzyZrealizowane)
                .GroupBy(z => z.Towar.Nazwa)
                .Select(g => new { Nazwa = g.Key, Ilosc = g.Sum(z => z.Ilosc) })
                .OrderByDescending(g => g.Ilosc)
                .FirstOrDefaultAsync();

            // Najrzadziej zamawiany towar w ostatnich 30 dniach
            var najrzadziejZamawiany = await _context.Zamowienie
                .Where(z => z.DataZamowienia >= trzydziesciDni && !z.CzyAnulowane && z.CzyZrealizowane)
                .GroupBy(z => z.Towar.Nazwa)
                .Select(g => new { Nazwa = g.Key, Ilosc = g.Sum(z => z.Ilosc) })
                .OrderBy(g => g.Ilosc)
                .FirstOrDefaultAsync();

            // Najczęściej zamawiane miasto i kod pocztowy
            var najczesciejMiasto = await _context.Zamowienie
                .Where(z => !z.CzyAnulowane && z.CzyZrealizowane)
                .GroupBy(z => new { z.Miasto, z.KodPocztowy })
                .Select(g => new { g.Key.Miasto, g.Key.KodPocztowy, Liczba = g.Count() })
                .OrderByDescending(g => g.Liczba)
                .FirstOrDefaultAsync();

            // Najrzadziej zamawiane miasto i kod pocztowy
            var najrzadziejMiasto = await _context.Zamowienie
                .Where(z => !z.CzyAnulowane && z.CzyZrealizowane)
                .GroupBy(z => new { z.Miasto, z.KodPocztowy })
                .Select(g => new { g.Key.Miasto, g.Key.KodPocztowy, Liczba = g.Count() })
                .OrderBy(g => g.Liczba)
                .FirstOrDefaultAsync();

            // POBIERZ LISTĘ ZAMÓWIEŃ DO WIDOKU
            var zamowienia = await _context.Zamowienie
                .Include(z => z.Towar)
                .Where(z => z.CzyZrealizowane)
                .OrderByDescending(z => z.DataZamowienia)
                .ToListAsync();

            ViewData["SprzedazDzisiaj"] = sprzedazDzisiaj;
            ViewData["ProduktySprzedaneDzisiaj"] = produktySprzedaneDzisiaj;
            ViewData["NajczesciejTowar"] = najczesciejZamawiany?.Nazwa ?? "-";
            ViewData["NajczesciejTowarIlosc"] = najczesciejZamawiany?.Ilosc ?? 0;
            ViewData["NajrzadziejTowar"] = najrzadziejZamawiany?.Nazwa ?? "-";
            ViewData["NajrzadziejTowarIlosc"] = najrzadziejZamawiany?.Ilosc ?? 0;
            ViewData["NajczesciejMiasto"] = najczesciejMiasto?.Miasto ?? "-";
            ViewData["NajczesciejKod"] = najczesciejMiasto?.KodPocztowy ?? "-";
            ViewData["NajczesciejMiastoIlosc"] = najczesciejMiasto?.Liczba ?? 0;
            ViewData["NajrzadziejMiasto"] = najrzadziejMiasto?.Miasto ?? "-";
            ViewData["NajrzadziejKod"] = najrzadziejMiasto?.KodPocztowy ?? "-";
            ViewData["NajrzadziejMiastoIlosc"] = najrzadziejMiasto?.Liczba ?? 0;

            return View("Index", zamowienia);
        }
        [HttpPost]
        public async Task<IActionResult> GenerujRaport(DateTime? dataOd, DateTime? dataDo)
        {
            DateTime od = dataOd ?? DateTime.Today.AddDays(-30);
            DateTime doD = dataDo ?? DateTime.Today;

            var zamowienia = await _context.Zamowienie
                .Include(z => z.Towar)
                .Where(z => z.DataZamowienia >= od && z.DataZamowienia <= doD)
                .OrderByDescending(z => z.DataZamowienia)
                .ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine("Produkt;Ilość;Przychód;Data sprzedaży");
            foreach (var z in zamowienia)
            {
                sb.AppendLine($"{z.Towar?.Nazwa};{z.Ilosc};{(z.Towar?.Cena ?? 0) * z.Ilosc};{z.DataZamowienia:yyyy-MM-dd}");
            }
            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", "raport.csv");
        }
        [HttpPost]
        public async Task<IActionResult> GenerujRaportPdf(DateTime? dataOd, DateTime? dataDo)
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            DateTime od = dataOd ?? DateTime.Today.AddDays(-30);
            DateTime doD = dataDo ?? DateTime.Today;

            var zamowienia = await _context.Zamowienie
                .Include(z => z.Towar)
                .Where(z => z.CzyZrealizowane && z.DataZamowienia >= od && z.DataZamowienia <= doD)
                .OrderByDescending(z => z.DataZamowienia)
                .ToListAsync();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);
                    page.Header()
                        .Text($"Raport zamówień zrealizowanych ({od:yyyy-MM-dd} - {doD:yyyy-MM-dd})")
                        .FontSize(18)
                        .Bold()
                        .AlignCenter();

                    page.Content()
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(30); // #
                                columns.RelativeColumn(2);  // Produkt
                                columns.RelativeColumn(1);  // Ilość
                                columns.RelativeColumn(1);  // Przychód
                                columns.RelativeColumn(1);  // Data
                            });

                            // Nagłówki
                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("#");
                                header.Cell().Element(CellStyle).Text("Produkt");
                                header.Cell().Element(CellStyle).Text("Ilość");
                                header.Cell().Element(CellStyle).Text("Przychód");
                                header.Cell().Element(CellStyle).Text("Data sprzedaży");
                            });

                            // Wiersze
                            int idx = 1;
                            foreach (var z in zamowienia)
                            {
                                table.Cell().Element(CellStyle).Text(idx.ToString());
                                table.Cell().Element(CellStyle).Text(z.Towar?.Nazwa ?? "-");
                                table.Cell().Element(CellStyle).Text(z.Ilosc.ToString());
                                table.Cell().Element(CellStyle).Text($"{(z.Towar?.Cena ?? 0) * z.Ilosc:C}");
                                table.Cell().Element(CellStyle).Text(z.DataZamowienia.ToString("yyyy-MM-dd"));
                                idx++;
                            }

                            static IContainer CellStyle(IContainer container) =>
                                container.PaddingVertical(2).PaddingHorizontal(4);
                        });
                });
            });

            var pdfBytes = document.GeneratePdf();
            return File(pdfBytes, "application/pdf", "raport_zamowien.pdf");
        }


    }
}