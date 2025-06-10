using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firma.Data.Data;
using Firma.Data.Data.Sklep;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace Firma.Intranet.Controllers
{
    [Authorize]
    public class ProduktPromocjaController : Controller
    {
        private readonly FirmaContext _context;

        public ProduktPromocjaController(FirmaContext context)
        {
            _context = context;
        }

        // GET: ProduktPromocja
        public async Task<IActionResult> Index()
        {
            var firmaIntranetContext = _context.ProduktPromocja.Include(p => p.Promocja).Include(p => p.Towar);
            return View(await firmaIntranetContext.ToListAsync());
        }

        // GET: ProduktPromocja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produktPromocja = await _context.ProduktPromocja
                .Include(p => p.Promocja)
                .Include(p => p.Towar)
                .FirstOrDefaultAsync(m => m.IdProduktyPromocji == id);
            if (produktPromocja == null)
            {
                return NotFound();
            }

            return View(produktPromocja);
        }

        // GET: ProduktPromocja/Create
        public IActionResult Create()
        {
            ViewData["IdPromocji"] = new SelectList(_context.Set<Promocja>(), "IdPromocji", "Nazwa");
            ViewData["IdTowaru"] = new SelectList(_context.Towar, "idTowar", "Kod");
            return View();
        }

        // POST: ProduktPromocja/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduktyPromocji,IdTowaru,IdPromocji")] ProduktPromocja produktPromocja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produktPromocja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPromocji"] = new SelectList(_context.Set<Promocja>(), "IdPromocji", "Nazwa", produktPromocja.IdPromocji);
            ViewData["IdTowaru"] = new SelectList(_context.Towar, "idTowar", "Kod", produktPromocja.IdTowaru);
            return View(produktPromocja);
        }

        // GET: ProduktPromocja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produktPromocja = await _context.ProduktPromocja.FindAsync(id);
            if (produktPromocja == null)
            {
                return NotFound();
            }
            ViewData["IdPromocji"] = new SelectList(_context.Set<Promocja>(), "IdPromocji", "Nazwa", produktPromocja.IdPromocji);
            ViewData["IdTowaru"] = new SelectList(_context.Towar, "idTowar", "Kod", produktPromocja.IdTowaru);
            return View(produktPromocja);
        }

        // POST: ProduktPromocja/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProduktyPromocji,IdTowaru,IdPromocji")] ProduktPromocja produktPromocja)
        {
            if (id != produktPromocja.IdProduktyPromocji)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produktPromocja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduktPromocjaExists(produktPromocja.IdProduktyPromocji))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPromocji"] = new SelectList(_context.Set<Promocja>(), "IdPromocji", "Nazwa", produktPromocja.IdPromocji);
            ViewData["IdTowaru"] = new SelectList(_context.Towar, "idTowar", "Kod", produktPromocja.IdTowaru);
            return View(produktPromocja);
        }

        // GET: ProduktPromocja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produktPromocja = await _context.ProduktPromocja
                .Include(p => p.Promocja)
                .Include(p => p.Towar)
                .FirstOrDefaultAsync(m => m.IdProduktyPromocji == id);
            if (produktPromocja == null)
            {
                return NotFound();
            }

            return View(produktPromocja);
        }

        // POST: ProduktPromocja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produktPromocja = await _context.ProduktPromocja.FindAsync(id);
            if (produktPromocja != null)
            {
                _context.ProduktPromocja.Remove(produktPromocja);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduktPromocjaExists(int id)
        {
            return _context.ProduktPromocja.Any(e => e.IdProduktyPromocji == id);
        }
    }
}
