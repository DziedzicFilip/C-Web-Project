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
    public class RecenzjaController : Controller
    {
        private readonly FirmaContext _context;

        public RecenzjaController(FirmaContext context)
        {
            _context = context;
        }

        // GET: Recenzja
        public async Task<IActionResult> Index()
        {
            var firmaIntranetContext = _context.Recenzja.Include(r => r.Towar).Include(r => r.Uzytkownik);
            return View(await firmaIntranetContext.ToListAsync());
        }

        // GET: Recenzja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recenzja = await _context.Recenzja
                .Include(r => r.Towar)
                .Include(r => r.Uzytkownik)
                .FirstOrDefaultAsync(m => m.IdRecenzji == id);
            if (recenzja == null)
            {
                return NotFound();
            }

            return View(recenzja);
        }

        // GET: Recenzja/Create
        public IActionResult Create()
        {
            ViewData["IdTowaru"] = new SelectList(_context.Towar, "idTowar", "Nazwa");
            ViewData["IdUzytkownika"] = new SelectList(_context.Set<Uzytkownik>(), "IdUzytkownika", "Email");
            return View();
        }

        // POST: Recenzja/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRecenzji,Ocena,Komentarz,DataDodania,IdTowaru,IdUzytkownika")] Recenzja recenzja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recenzja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTowaru"] = new SelectList(_context.Towar, "idTowar", "Nazwa", recenzja.IdTowaru);
            ViewData["IdUzytkownika"] = new SelectList(_context.Set<Uzytkownik>(), "IdUzytkownika", "Email", recenzja.IdUzytkownika);
            return View(recenzja);
        }

        // GET: Recenzja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recenzja = await _context.Recenzja.FindAsync(id);
            if (recenzja == null)
            {
                return NotFound();
            }
            ViewData["IdTowaru"] = new SelectList(_context.Towar, "idTowar", "Nazwa", recenzja.IdTowaru);
            ViewData["IdUzytkownika"] = new SelectList(_context.Set<Uzytkownik>(), "IdUzytkownika", "Email", recenzja.IdUzytkownika);
            return View(recenzja);
        }

        // POST: Recenzja/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRecenzji,Ocena,Komentarz,DataDodania,IdTowaru,IdUzytkownika")] Recenzja recenzja)
        {
            if (id != recenzja.IdRecenzji)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recenzja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecenzjaExists(recenzja.IdRecenzji))
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
            ViewData["IdTowaru"] = new SelectList(_context.Towar, "idTowar", "Nazwa", recenzja.IdTowaru);
            ViewData["IdUzytkownika"] = new SelectList(_context.Set<Uzytkownik>(), "IdUzytkownika", "Email", recenzja.IdUzytkownika);
            return View(recenzja);
        }

        // GET: Recenzja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recenzja = await _context.Recenzja
                .Include(r => r.Towar)
                .Include(r => r.Uzytkownik)
                .FirstOrDefaultAsync(m => m.IdRecenzji == id);
            if (recenzja == null)
            {
                return NotFound();
            }

            return View(recenzja);
        }

        // POST: Recenzja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recenzja = await _context.Recenzja.FindAsync(id);
            if (recenzja != null)
            {
                _context.Recenzja.Remove(recenzja);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecenzjaExists(int id)
        {
            return _context.Recenzja.Any(e => e.IdRecenzji == id);
        }
    }
}
