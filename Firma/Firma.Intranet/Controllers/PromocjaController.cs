using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Firma.Intranet.Data;
using Firma.Intranet.Models.Sklep;

namespace Firma.Intranet.Controllers
{
    public class PromocjaController : Controller
    {
        private readonly FirmaIntranetContext _context;

        public PromocjaController(FirmaIntranetContext context)
        {
            _context = context;
        }

        // GET: Promocja
        public async Task<IActionResult> Index()
        {
            return View(await _context.Promocja.ToListAsync());
        }

        // GET: Promocja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promocja = await _context.Promocja
                .FirstOrDefaultAsync(m => m.IdPromocji == id);
            if (promocja == null)
            {
                return NotFound();
            }

            return View(promocja);
        }

        // GET: Promocja/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Promocja/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPromocji,Nazwa,Rabat,DataRozpoczecia,DataZakonczenia,CzyAktywna")] Promocja promocja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(promocja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(promocja);
        }

        // GET: Promocja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promocja = await _context.Promocja.FindAsync(id);
            if (promocja == null)
            {
                return NotFound();
            }
            return View(promocja);
        }

        // POST: Promocja/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPromocji,Nazwa,Rabat,DataRozpoczecia,DataZakonczenia,CzyAktywna")] Promocja promocja)
        {
            if (id != promocja.IdPromocji)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promocja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromocjaExists(promocja.IdPromocji))
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
            return View(promocja);
        }

        // GET: Promocja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promocja = await _context.Promocja
                .FirstOrDefaultAsync(m => m.IdPromocji == id);
            if (promocja == null)
            {
                return NotFound();
            }

            return View(promocja);
        }

        // POST: Promocja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var promocja = await _context.Promocja.FindAsync(id);
            if (promocja != null)
            {
                _context.Promocja.Remove(promocja);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromocjaExists(int id)
        {
            return _context.Promocja.Any(e => e.IdPromocji == id);
        }
    }
}
