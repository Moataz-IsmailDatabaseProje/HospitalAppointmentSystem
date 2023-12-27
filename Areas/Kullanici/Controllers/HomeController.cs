using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


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
            var eFHastaneRandevuContext = _context.Randevular.Include(r => r.Doktor).Include(r => r.User);
            return View(await eFHastaneRandevuContext.ToListAsync());
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
       
    }
}