﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HastaneRandevuSistemi.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace HastaneRandevuSistemi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class PoliklinikController : Controller
    {
        private readonly EFHastaneRandevuContext _context;
        public PoliklinikController(EFHastaneRandevuContext context, IStringLocalizer<PoliklinikController> stringLocalizer)
        {
            _context = context;
        }

        // GET: Admin/Poliklinik
        public async Task<IActionResult> Index()
        {
            List<Poliklinik> polikliniks = new List<Poliklinik>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync("http://localhost:5107/api/PoliklinikApi");

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        polikliniks = JsonConvert.DeserializeObject<List<Poliklinik>>(jsonResponse);
                    }
                    else
                    {
                        // Handle API error accordingly
                        return View("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return View("Error");
            }

            return View(polikliniks != null ? polikliniks : new List<Poliklinik>());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync($"http://localhost:5107/api/PoliklinikApi/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var poliklinik = JsonConvert.DeserializeObject<Poliklinik>(jsonResponse);

                        if (poliklinik == null)
                        {
                            return NotFound();
                        }

                        return View(poliklinik);
                    }
                    else
                    {
                        // Handle API error accordingly
                        return View("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return View("Error");
            }
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
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var content = new StringContent(JsonConvert.SerializeObject(poliklinik), Encoding.UTF8, "application/json");

                        var response = await client.PostAsync("http://localhost:5107/api/PoliklinikApi", content);

                        if (response.IsSuccessStatusCode)
                        {
                            // Successfully created, you can handle the response if needed
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            // Handle API error accordingly
                            return View("Error");
                        }
                    }
                }
                else
                {
                    // Handle invalid model state (e.g., validation errors)
                    return View(poliklinik);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine(ex.ToString());
                return View("Error");
            }
        }


        // GET: Admin/Poliklinik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Adi")] Poliklinik poliklinik)
        {
            if (id != poliklinik.Id)
            {
                return NotFound();
            }

         
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var content = new StringContent(JsonConvert.SerializeObject(poliklinik), Encoding.UTF8, "application/json");

                        // Make a PUT request to the API endpoint
                        var response = await client.PutAsync($"http://localhost:5107/api/PoliklinikApi/{id}", content);

                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            // Handle API error accordingly
                            return View("Error");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as needed
                    Console.WriteLine(ex.ToString());
                    return View("Error");
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
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Make a DELETE request to the API
                    var response = await client.DeleteAsync($"http://localhost:5107/api/PoliklinikApi/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        // Successfully deleted, redirect to Index
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        // Handle API error
                        return View("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine(ex.ToString());
                return View("Error");
            }
        }



        private bool PoliklinikExists(int id)
        {
          return (_context.Poliklinikler?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
