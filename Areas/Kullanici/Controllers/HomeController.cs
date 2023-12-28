using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace HastaneRandevuSistemi.Controllers
{
    [Area("Kullanici")]
    public class HomeController : Controller
    {
        private readonly EFHastaneRandevuContext _context;

        public HomeController(EFHastaneRandevuContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get the currently logged-in user's Id
            string currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Query appointments for the current user
            var appointmentsForCurrentUser = _context.Randevular
                .Include(r => r.Doktor)
                .Include(r => r.User)
                .Where(r => r.UserId == currentUserId);

            return View(await appointmentsForCurrentUser.ToListAsync());
        }


        // GET: Admin/Randevu/Create
        public IActionResult Create()
        {
            // Get the currently logged-in user's Id
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            // Set the current user's Id in the ViewBag
            ViewBag.CurrentUserId = currentUserId;

            // Populate dropdowns
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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