using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Firma.Data.Data;
using Firma.Data.Data.CMS;
using Microsoft.AspNetCore.Authorization;

namespace Firma.Intranet.Controllers
{
    [Authorize]
    public class PrzydatneLinkiController : Controller
    {
        private readonly FirmaContext _context;

        public PrzydatneLinkiController(FirmaContext context)
        {
            _context = context;
        }

        // GET: PrzydatneLinki
        public async Task<IActionResult> Index()
        {
            return View(await _context.PrzydatneLinki.ToListAsync());
        }

        // GET: PrzydatneLinki/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przydatneLinki = await _context.PrzydatneLinki
                .FirstOrDefaultAsync(m => m.IdPrzydatneLinki == id);
            if (przydatneLinki == null)
            {
                return NotFound();
            }

            return View(przydatneLinki);
        }

        // GET: PrzydatneLinki/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PrzydatneLinki/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPrzydatneLinki,Tytul,Link")] PrzydatneLinki przydatneLinki)
        {
            if (ModelState.IsValid)
            {
                _context.Add(przydatneLinki);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(przydatneLinki);
        }

        // GET: PrzydatneLinki/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przydatneLinki = await _context.PrzydatneLinki.FindAsync(id);
            if (przydatneLinki == null)
            {
                return NotFound();
            }
            return View(przydatneLinki);
        }

        // POST: PrzydatneLinki/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPrzydatneLinki,Tytul,Link")] PrzydatneLinki przydatneLinki)
        {
            if (id != przydatneLinki.IdPrzydatneLinki)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(przydatneLinki);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrzydatneLinkiExists(przydatneLinki.IdPrzydatneLinki))
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
            return View(przydatneLinki);
        }

        // GET: PrzydatneLinki/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przydatneLinki = await _context.PrzydatneLinki
                .FirstOrDefaultAsync(m => m.IdPrzydatneLinki == id);
            if (przydatneLinki == null)
            {
                return NotFound();
            }

            return View(przydatneLinki);
        }

        // POST: PrzydatneLinki/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var przydatneLinki = await _context.PrzydatneLinki.FindAsync(id);
            if (przydatneLinki != null)
            {
                _context.PrzydatneLinki.Remove(przydatneLinki);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrzydatneLinkiExists(int id)
        {
            return _context.PrzydatneLinki.Any(e => e.IdPrzydatneLinki == id);
        }
    }
}
