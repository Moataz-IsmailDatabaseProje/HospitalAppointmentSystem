using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authorization;

namespace HastaneRandevuSistemi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class RandevuController : Controller
    {
        private readonly EFHastaneRandevuContext _context;

        public RandevuController(EFHastaneRandevuContext context)
        {
            _context = context;
        }

        //public void OnGet()
        //{
        //    string? culture = Request.Query["culture"];
        //    Console.WriteLine("new selected language " + culture);
        //    if (culture != null)
        //    {
        //        Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
        //            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
        //            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

        //    }
        //    string returnUrl = Request.Headers["Referer"].ToString() ?? "/kullanici/index";
        //    Response.Redirect(returnUrl);
        //}

        // GET: Admin/Randevu
        public async Task<IActionResult> Index()
        {
            var eFHastaneRandevuContext = _context.Randevular.Include(r => r.Doktor).Include(r => r.Poliklinik).Include(r => r.User);
            return View(await eFHastaneRandevuContext.ToListAsync());
        }

        // GET: Admin/Randevu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Randevular == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular
                .Include(r => r.Doktor)
                .Include(r => r.Poliklinik)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // GET: Admin/Randevu/Create
        public IActionResult Create()
        {
            ViewData["DoktorId"] = new SelectList(_context.Doktorlar, "Id", "Adi");
            ViewData["PoliklinikId"] = new SelectList(_context.Poliklinikler, "Id", "Adi");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "UserName");

            // Populate PoliklinikId dropdown
            ViewBag.PoliklinikId = new SelectList(_context.Poliklinikler, "Id", "Adi");
            // Initialize DoktorId dropdown (empty for now)
            ViewBag.DoktorId = new SelectList(_context.Doktorlar, "Id", "Adi");
            // Populate UserId dropdown (you'll need to implement this based on your user data)
            ViewBag.UserId = new SelectList(_context.ApplicationUsers, "Id", "UserName");

            return View();
        }

        // POST: Admin/Randevu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tarih,DoktorId,UserId,PoliklinikId")] Randevu randevu)
        {
            bool hataVar = false;
            var appointmentDate = _context.Randevular.Where(x => x.Tarih == randevu.Tarih).Count();
            if (appointmentDate != 0)
            {
                ViewBag.Mesaj = "Hata doktorun bu saate başka bir randevusu var!";
                hataVar = true;
            }

            if (randevu.Tarih.Hour < 09.00 || randevu.Tarih.Hour > 17.00)
            {
                ViewBag.Mesaj = "9.00 - 17.00 arası olmalı";
                hataVar = true;
            }

            if (randevu.Tarih.DayOfWeek == DayOfWeek.Sunday || randevu.Tarih.DayOfWeek == DayOfWeek.Saturday)
            {
                ViewBag.Mesaj = "Hafta İçi Randevu Alınız!";
                hataVar = true;
            }

            if (randevu.Tarih.Date < DateTime.Today)
            {
                ViewBag.Mesaj = "Geçmiş Zamana Randevu alınmaz!";
                hataVar = true;
            }

            if (hataVar)
            {
                ViewData["PoliklinikId"] = new SelectList(_context.Poliklinikler, "Id", "Adi", randevu.PoliklinikId);
                ViewData["DoktorId"] = new SelectList(_context.Doktorlar, "Id", "Adi", randevu.DoktorId);
                ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "UserName", randevu.UserId);
                ViewBag.PoliklinikAdi = new SelectList(_context.Poliklinikler, "Id", "Adi", randevu.PoliklinikId);
                ViewBag.DoktorAdi = new SelectList(_context.Doktorlar, "Id", "Adi", randevu.DoktorId);
                ViewBag.UserName = new SelectList(_context.ApplicationUsers, "Id", "UserName", randevu.UserId);

                return View(randevu);
            }

            // Continue with the rest of the code for saving the changes
            _context.Add(randevu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }


        // GET: Admin/Randevu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Randevular == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu == null)
            {
                return NotFound();
            }
            ViewData["DoktorId"] = new SelectList(_context.Doktorlar, "Id", "Adi", randevu.DoktorId);
            ViewData["PoliklinikId"] = new SelectList(_context.Poliklinikler, "Id", "Adi", randevu.PoliklinikId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "UserName", randevu.UserId);
            return View(randevu);
        }

        // POST: Admin/Randevu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tarih,DoktorId,UserId,PoliklinikId")] Randevu randevu)
        {
            if (id != randevu.Id)
            {
                return NotFound();
            }

            bool hataVar = false;
            var appointmentDate = _context.Randevular.Where(x => x.Tarih == randevu.Tarih && x.Id != id).Count();
            if (appointmentDate != 0)
            {
                ViewBag.Mesaj = "Bu saatte randevu vardır, lütfen başka bir saat deneyin.";
                hataVar = true;
            }

            if (randevu.Tarih.Hour < 09.00 || randevu.Tarih.Hour > 17.00)
            {
                ViewBag.Mesaj = "9.00 - 17.00 arası olmalı";
                hataVar = true;
            }

            if (randevu.Tarih.DayOfWeek == DayOfWeek.Sunday || randevu.Tarih.DayOfWeek == DayOfWeek.Saturday)
            {
                ViewBag.Mesaj = "Hafta İçi Randevu Alınız!";
                hataVar = true;
            }

            if (randevu.Tarih.Date < DateTime.Today)
            {
                ViewBag.Mesaj = "Geçmiş Zamana Randevu alınmaz!";
                hataVar = true;
            }

            if (!hataVar)
            {
                try
                {
                    _context.Update(randevu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RandevuExists(randevu.Id))
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

            ViewData["DoktorId"] = new SelectList(_context.Doktorlar, "Id", "Adi", randevu.DoktorId);
            ViewData["PoliklinikId"] = new SelectList(_context.Poliklinikler, "Id", "Adi", randevu.PoliklinikId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "UserName", randevu.UserId);
            ViewBag.PoliklinikAdi = new SelectList(_context.Poliklinikler, "Id", "Adi", randevu.PoliklinikId);
            ViewBag.DoktorAdi = new SelectList(_context.Doktorlar, "Id", "Adi", randevu.DoktorId);
            ViewBag.UserName = new SelectList(_context.ApplicationUsers, "Id", "UserName", randevu.UserId);

            return View(randevu);
        }


        // GET: Admin/Randevu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Randevular == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular
                .Include(r => r.Doktor)
                .Include(r => r.Poliklinik)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // POST: Admin/Randevu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Randevular == null)
            {
                return Problem("Entity set 'EFHastaneRandevuContext.Randevular'  is null.");
            }
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu != null)
            {
                _context.Randevular.Remove(randevu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RandevuExists(int id)
        {
          return (_context.Randevular?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet]
        public IActionResult GetDoctors(int poliklinikId)
        {
            var doctors = _context.Doktorlar
                .Where(d => d.PoliklinikId == poliklinikId)
                .Select(d => new
                {
                    doctorId = d.Id,
                    doctorName = $"{d.Adi} {d.Soyadi}"
                })
                .ToList();

            return Json(doctors);
        }


    }
}
