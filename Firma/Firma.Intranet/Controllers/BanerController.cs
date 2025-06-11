using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firma.Data.Data;
using Firma.Data.Data.CMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Firma.Intranet.Controllers
{
    [Authorize]
    public class BanerController : Controller
    {
        private readonly FirmaContext _context;

        public BanerController(FirmaContext context)
        {
            _context = context;
        }

        // GET: Baner
        public async Task<IActionResult> Index()
        {
            return View(await _context.Baner.ToListAsync());
        }

        // GET: Baner/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baner = await _context.Baner
                .FirstOrDefaultAsync(m => m.IdBanera == id);
            if (baner == null)
            {
                return NotFound();
            }

            return View(baner);
        }

        // GET: Baner/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Baner/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBanera,Tytul,Zawartosc,DataPoczatkowa,DataZakonczenia,CzyAktywny")] Baner baner, IFormFile plik)
        {
            if (ModelState.IsValid)
            {
                if (plik != null && plik.Length > 0)
                {
                    string sciezkaFolder = @"D:\Studia\WSB\IAB\C-Web-Project\Firma\Firma.Intranet\wwwroot\images\Banners";
                    string fileName = Guid.NewGuid() + "_" + Path.GetFileName(plik.FileName);
                    string filePath = Path.Combine(sciezkaFolder, fileName);

                    if (!Directory.Exists(sciezkaFolder))
                        Directory.CreateDirectory(sciezkaFolder);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await plik.CopyToAsync(stream);
                    }

                    baner.UrlObrazka = Path.Combine("images", "Banners", fileName).Replace("\\", "/");
                }

                _context.Add(baner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(baner);
        }


        // GET: Baner/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baner = await _context.Baner.FindAsync(id);
            if (baner == null)
            {
                return NotFound();
            }
            return View(baner);
        }

        // POST: Baner/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBanera,Tytul,Zawartosc,DataPoczatkowa,DataZakonczenia,CzyAktywny")] Baner baner, IFormFile plik)
        {
            if (id != baner.IdBanera)
                return NotFound();

            if (ModelState.IsValid)
            {
                var banerDb = await _context.Baner.AsNoTracking().FirstOrDefaultAsync(b => b.IdBanera == id);
                if (banerDb == null)
                    return NotFound();

                if (plik != null && plik.Length > 0)
                {
                    string sciezkaFolder = @"D:\Studia\WSB\IAB\C-Web-Project\Firma\Firma.Intranet\wwwroot\images\Banners";
                    string fileName = Guid.NewGuid() + "_" + Path.GetFileName(plik.FileName);
                    string filePath = Path.Combine(sciezkaFolder, fileName);

                    if (!Directory.Exists(sciezkaFolder))
                        Directory.CreateDirectory(sciezkaFolder);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await plik.CopyToAsync(stream);
                    }

                    baner.UrlObrazka = Path.Combine("images", "Banners", fileName).Replace("\\", "/");
                }
                else
                {
                    baner.UrlObrazka = banerDb.UrlObrazka;
                }

                _context.Update(baner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(baner);
        }


        // GET: Baner/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baner = await _context.Baner
                .FirstOrDefaultAsync(m => m.IdBanera == id);
            if (baner == null)
            {
                return NotFound();
            }

            return View(baner);
        }

        // POST: Baner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var baner = await _context.Baner.FindAsync(id);
            if (baner != null)
            {
                _context.Baner.Remove(baner);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BanerExists(int id)
        {
            return _context.Baner.Any(e => e.IdBanera == id);
        }
    }
}
