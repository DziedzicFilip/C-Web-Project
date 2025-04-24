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
    public class ZamowienieTowarController : Controller
    {
        private readonly FirmaIntranetContext _context;

        public ZamowienieTowarController(FirmaIntranetContext context)
        {
            _context = context;
        }

        // GET: ZamowienieTowar
        public async Task<IActionResult> Index()
        {
            var firmaIntranetContext = _context.ZamowienieTowar.Include(z => z.Towar).Include(z => z.Zamowienie);
            return View(await firmaIntranetContext.ToListAsync());
        }

        // GET: ZamowienieTowar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zamowienieTowar = await _context.ZamowienieTowar
                .Include(z => z.Towar)
                .Include(z => z.Zamowienie)
                .FirstOrDefaultAsync(m => m.IdZamowienieTowar == id);
            if (zamowienieTowar == null)
            {
                return NotFound();
            }

            return View(zamowienieTowar);
        }

        // GET: ZamowienieTowar/Create
        public IActionResult Create()
        {
            ViewData["IdTowaru"] = new SelectList(_context.Towar, "idTowar", "FotoUrl");
            ViewData["IdZamowienia"] = new SelectList(_context.Zamowienie, "IdZamowienia", "KodPocztowy");
            return View();
        }

        // POST: ZamowienieTowar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdZamowienieTowar,IdZamowienia,IdTowaru,Ilosc")] ZamowienieTowar zamowienieTowar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zamowienieTowar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTowaru"] = new SelectList(_context.Towar, "idTowar", "FotoUrl", zamowienieTowar.IdTowaru);
            ViewData["IdZamowienia"] = new SelectList(_context.Zamowienie, "IdZamowienia", "KodPocztowy", zamowienieTowar.IdZamowienia);
            return View(zamowienieTowar);
        }

        // GET: ZamowienieTowar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zamowienieTowar = await _context.ZamowienieTowar.FindAsync(id);
            if (zamowienieTowar == null)
            {
                return NotFound();
            }
            ViewData["IdTowaru"] = new SelectList(_context.Towar, "idTowar", "FotoUrl", zamowienieTowar.IdTowaru);
            ViewData["IdZamowienia"] = new SelectList(_context.Zamowienie, "IdZamowienia", "KodPocztowy", zamowienieTowar.IdZamowienia);
            return View(zamowienieTowar);
        }

        // POST: ZamowienieTowar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdZamowienieTowar,IdZamowienia,IdTowaru,Ilosc")] ZamowienieTowar zamowienieTowar)
        {
            if (id != zamowienieTowar.IdZamowienieTowar)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zamowienieTowar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZamowienieTowarExists(zamowienieTowar.IdZamowienieTowar))
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
            ViewData["IdTowaru"] = new SelectList(_context.Towar, "idTowar", "FotoUrl", zamowienieTowar.IdTowaru);
            ViewData["IdZamowienia"] = new SelectList(_context.Zamowienie, "IdZamowienia", "KodPocztowy", zamowienieTowar.IdZamowienia);
            return View(zamowienieTowar);
        }

        // GET: ZamowienieTowar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zamowienieTowar = await _context.ZamowienieTowar
                .Include(z => z.Towar)
                .Include(z => z.Zamowienie)
                .FirstOrDefaultAsync(m => m.IdZamowienieTowar == id);
            if (zamowienieTowar == null)
            {
                return NotFound();
            }

            return View(zamowienieTowar);
        }

        // POST: ZamowienieTowar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zamowienieTowar = await _context.ZamowienieTowar.FindAsync(id);
            if (zamowienieTowar != null)
            {
                _context.ZamowienieTowar.Remove(zamowienieTowar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZamowienieTowarExists(int id)
        {
            return _context.ZamowienieTowar.Any(e => e.IdZamowienieTowar == id);
        }
    }
}
