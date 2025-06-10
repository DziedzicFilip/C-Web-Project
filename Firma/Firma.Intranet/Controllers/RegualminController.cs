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
    public class RegualminController : Controller
    {
        private readonly FirmaContext _context;

        public RegualminController(FirmaContext context)
        {
            _context = context;
        }

        // GET: Regualmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Regualmin.ToListAsync());
        }

        // GET: Regualmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regualmin = await _context.Regualmin
                .FirstOrDefaultAsync(m => m.IdRegulamin == id);
            if (regualmin == null)
            {
                return NotFound();
            }

            return View(regualmin);
        }

        // GET: Regualmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Regualmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRegulamin,Tytul,Tresc")] Regualmin regualmin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(regualmin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(regualmin);
        }

        // GET: Regualmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regualmin = await _context.Regualmin.FindAsync(id);
            if (regualmin == null)
            {
                return NotFound();
            }
            return View(regualmin);
        }

        // POST: Regualmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRegulamin,Tytul,Tresc")] Regualmin regualmin)
        {
            if (id != regualmin.IdRegulamin)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(regualmin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegualminExists(regualmin.IdRegulamin))
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
            return View(regualmin);
        }

        // GET: Regualmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regualmin = await _context.Regualmin
                .FirstOrDefaultAsync(m => m.IdRegulamin == id);
            if (regualmin == null)
            {
                return NotFound();
            }

            return View(regualmin);
        }

        // POST: Regualmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var regualmin = await _context.Regualmin.FindAsync(id);
            if (regualmin != null)
            {
                _context.Regualmin.Remove(regualmin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegualminExists(int id)
        {
            return _context.Regualmin.Any(e => e.IdRegulamin == id);
        }
    }
}
