﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Authorization;

namespace HastaneRandevuSistemi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class AnaBilimDaliController : Controller
    {
        private readonly EFHastaneRandevuContext _context;

        public AnaBilimDaliController(EFHastaneRandevuContext context)
        {
            _context = context;
        }

        // GET: AnaBilimDali
        public async Task<IActionResult> Index()
        {
            return _context.AnaBilimDaliler != null ?
                        View(await _context.AnaBilimDaliler.ToListAsync()) :
                        Problem("Entity set 'EFHastaneRandevuContext.AnaBilimDaliler'  is null.");
        }

        // GET: AnaBilimDali/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnaBilimDaliler == null)
            {
                return NotFound();
            }

            var anaBilimDali = await _context.AnaBilimDaliler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anaBilimDali == null)
            {
                return NotFound();
            }

            return View(anaBilimDali);
        }

        // GET: AnaBilimDali/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnaBilimDali/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Adi")] AnaBilimDali anaBilimDali)
        {
            if (ModelState.IsValid || true)
            {
                _context.Add(anaBilimDali);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(anaBilimDali);
        }

        // GET: AnaBilimDali/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AnaBilimDaliler == null)
            {
                return NotFound();
            }

            var anaBilimDali = await _context.AnaBilimDaliler.FindAsync(id);
            if (anaBilimDali == null)
            {
                return NotFound();
            }
            return View(anaBilimDali);
        }

        // POST: AnaBilimDali/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Adi")] AnaBilimDali anaBilimDali)
        {
            if (id != anaBilimDali.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid || true)
            {
                try
                {
                    _context.Update(anaBilimDali);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnaBilimDaliExists(anaBilimDali.Id))
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
            return View(anaBilimDali);
        }

        // GET: AnaBilimDali/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AnaBilimDaliler == null)
            {
                return NotFound();
            }

            var anaBilimDali = await _context.AnaBilimDaliler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anaBilimDali == null)
            {
                return NotFound();
            }

            return View(anaBilimDali);
        }

        // POST: AnaBilimDali/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AnaBilimDaliler == null)
            {
                return Problem("Entity set 'EFHastaneRandevuContext.AnaBilimDaliler'  is null.");
            }
            var anaBilimDali = await _context.AnaBilimDaliler.FindAsync(id);
            if (anaBilimDali != null)
            {
                _context.AnaBilimDaliler.Remove(anaBilimDali);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnaBilimDaliExists(int id)
        {
            return (_context.AnaBilimDaliler?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
