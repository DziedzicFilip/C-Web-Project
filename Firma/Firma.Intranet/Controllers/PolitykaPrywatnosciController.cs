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
    public class PolitykaPrywatnosciController : Controller
    {
        private readonly FirmaContext _context;

        public PolitykaPrywatnosciController(FirmaContext context)
        {
            _context = context;
        }

        // GET: PolitykaPrywatnosci
        public async Task<IActionResult> Index()
        {
            return View(await _context.PolitykaPrywatnosci.ToListAsync());
        }

        // GET: PolitykaPrywatnosci/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var politykaPrywatnosci = await _context.PolitykaPrywatnosci
                .FirstOrDefaultAsync(m => m.Id == id);
            if (politykaPrywatnosci == null)
            {
                return NotFound();
            }

            return View(politykaPrywatnosci);
        }

        // GET: PolitykaPrywatnosci/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PolitykaPrywatnosci/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tytul,Tresc,DataUtworzenia,Aktywna")] PolitykaPrywatnosci politykaPrywatnosci)
        {
            if (ModelState.IsValid)
            {
                _context.Add(politykaPrywatnosci);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(politykaPrywatnosci);
        }

        // GET: PolitykaPrywatnosci/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var politykaPrywatnosci = await _context.PolitykaPrywatnosci.FindAsync(id);
            if (politykaPrywatnosci == null)
            {
                return NotFound();
            }
            return View(politykaPrywatnosci);
        }

        // POST: PolitykaPrywatnosci/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tytul,Tresc,DataUtworzenia,Aktywna")] PolitykaPrywatnosci politykaPrywatnosci)
        {
            if (id != politykaPrywatnosci.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(politykaPrywatnosci);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolitykaPrywatnosciExists(politykaPrywatnosci.Id))
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
            return View(politykaPrywatnosci);
        }

        // GET: PolitykaPrywatnosci/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var politykaPrywatnosci = await _context.PolitykaPrywatnosci
                .FirstOrDefaultAsync(m => m.Id == id);
            if (politykaPrywatnosci == null)
            {
                return NotFound();
            }

            return View(politykaPrywatnosci);
        }

        // POST: PolitykaPrywatnosci/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var politykaPrywatnosci = await _context.PolitykaPrywatnosci.FindAsync(id);
            if (politykaPrywatnosci != null)
            {
                _context.PolitykaPrywatnosci.Remove(politykaPrywatnosci);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolitykaPrywatnosciExists(int id)
        {
            return _context.PolitykaPrywatnosci.Any(e => e.Id == id);
        }
    }
}
