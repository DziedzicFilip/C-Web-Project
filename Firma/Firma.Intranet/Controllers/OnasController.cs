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
    public class OnasController : Controller
    {
        private readonly FirmaContext _context;

        public OnasController(FirmaContext context)
        {
            _context = context;
        }

        // GET: Onas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Onas.ToListAsync());
        }

        // GET: Onas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onas = await _context.Onas
                .FirstOrDefaultAsync(m => m.IdOnas == id);
            if (onas == null)
            {
                return NotFound();
            }

            return View(onas);
        }

        // GET: Onas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Onas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOnas,Name,Description")] Onas onas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(onas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(onas);
        }

        // GET: Onas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onas = await _context.Onas.FindAsync(id);
            if (onas == null)
            {
                return NotFound();
            }
            return View(onas);
        }

        // POST: Onas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOnas,Name,Description")] Onas onas)
        {
            if (id != onas.IdOnas)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(onas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OnasExists(onas.IdOnas))
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
            return View(onas);
        }

        // GET: Onas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onas = await _context.Onas
                .FirstOrDefaultAsync(m => m.IdOnas == id);
            if (onas == null)
            {
                return NotFound();
            }

            return View(onas);
        }

        // POST: Onas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var onas = await _context.Onas.FindAsync(id);
            if (onas != null)
            {
                _context.Onas.Remove(onas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OnasExists(int id)
        {
            return _context.Onas.Any(e => e.IdOnas == id);
        }
    }
}
