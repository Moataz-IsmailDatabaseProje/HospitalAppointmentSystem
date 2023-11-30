using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HastaneRandevuSistemi.Models;

namespace HastaneRandevuSistemi.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly EFHastaneRandevuContext _context;

        public KullaniciController(EFHastaneRandevuContext context)
        {
            _context = context;
        }

        // GET: Kullanici
        public async Task<IActionResult> Index()
        {
              return _context.Kullaniciler != null ? 
                          View(await _context.Kullaniciler.ToListAsync()) :
                          Problem("Entity set 'EFHastaneRandevuContext.Kullaniciler'  is null.");
        }

        // GET: Kullanici/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kullaniciler == null)
            {
                return NotFound();
            }

            var kullanici = await _context.Kullaniciler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kullanici == null)
            {
                return NotFound();
            }

            return View(kullanici);
        }

        // GET: Kullanici/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kullanici/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KullaniciAdi,Sifre")] Kullanici kullanici)
        {
            if (ModelState.IsValid || true)
            {
                _context.Add(kullanici);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kullanici);
        }

        // GET: Kullanici/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kullaniciler == null)
            {
                return NotFound();
            }

            var kullanici = await _context.Kullaniciler.FindAsync(id);
            if (kullanici == null)
            {
                return NotFound();
            }
            return View(kullanici);
        }

        // POST: Kullanici/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KullaniciAdi,Sifre")] Kullanici kullanici)
        {
            if (id != kullanici.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid || true)
            {
                try
                {
                    _context.Update(kullanici);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KullaniciExists(kullanici.Id))
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
            return View(kullanici);
        }

        // GET: Kullanici/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kullaniciler == null)
            {
                return NotFound();
            }

            var kullanici = await _context.Kullaniciler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kullanici == null)
            {
                return NotFound();
            }

            return View(kullanici);
        }

        // POST: Kullanici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kullaniciler == null)
            {
                return Problem("Entity set 'EFHastaneRandevuContext.Kullaniciler'  is null.");
            }
            var kullanici = await _context.Kullaniciler.FindAsync(id);
            if (kullanici != null)
            {
                _context.Kullaniciler.Remove(kullanici);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KullaniciExists(int id)
        {
          return (_context.Kullaniciler?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
