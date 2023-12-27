using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HastaneRandevuSistemi.Models;

namespace HastaneRandevuSistemi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PoliklinikController : Controller
    {
        private readonly EFHastaneRandevuContext _context;

        public PoliklinikController(EFHastaneRandevuContext context)
        {
            _context = context;
        }

        // GET: Admin/Poliklinik
        public async Task<IActionResult> Index()
        {
              return _context.Poliklinikler != null ? 
                          View(await _context.Poliklinikler.ToListAsync()) :
                          Problem("Entity set 'EFHastaneRandevuContext.Poliklinikler'  is null.");
        }

        // GET: Admin/Poliklinik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Poliklinikler == null)
            {
                return NotFound();
            }

            var poliklinik = await _context.Poliklinikler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poliklinik == null)
            {
                return NotFound();
            }

            return View(poliklinik);
        }

        // GET: Admin/Poliklinik/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Poliklinik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Adi")] Poliklinik poliklinik)
        {
            if (ModelState.IsValid || true)
            {
                _context.Add(poliklinik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(poliklinik);
        }

        // GET: Admin/Poliklinik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Poliklinikler == null)
            {
                return NotFound();
            }

            var poliklinik = await _context.Poliklinikler.FindAsync(id);
            if (poliklinik == null)
            {
                return NotFound();
            }
            return View(poliklinik);
        }

        // POST: Admin/Poliklinik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Adi")] Poliklinik poliklinik)
        {
            if (id != poliklinik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid || true)
            {
                try
                {
                    _context.Update(poliklinik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoliklinikExists(poliklinik.Id))
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
            return View(poliklinik);
        }

        // GET: Admin/Poliklinik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Poliklinikler == null)
            {
                return NotFound();
            }

            var poliklinik = await _context.Poliklinikler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poliklinik == null)
            {
                return NotFound();
            }

            return View(poliklinik);
        }

        // POST: Admin/Poliklinik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Poliklinikler == null)
            {
                return Problem("Entity set 'EFHastaneRandevuContext.Poliklinikler'  is null.");
            }
            var poliklinik = await _context.Poliklinikler.FindAsync(id);
            if (poliklinik != null)
            {
                _context.Poliklinikler.Remove(poliklinik);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PoliklinikExists(int id)
        {
          return (_context.Poliklinikler?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
