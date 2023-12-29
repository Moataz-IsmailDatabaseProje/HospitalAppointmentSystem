using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HastaneRandevuSistemi.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliklinikApiController : ControllerBase
    {
        private readonly EFHastaneRandevuContext _context;

        public PoliklinikApiController(EFHastaneRandevuContext context)
        {
            _context = context;
        }

        // GET: api/<PoliklinikApiController>
        [HttpGet]
        public List<Poliklinik> Get()
        {
            var polikliniks = _context.Poliklinikler.ToList();
            return polikliniks;
        }


        // GET api/<PoliklinikApiController>/5
        [HttpGet("{id}")]
        public ActionResult<Poliklinik> Get(int id)
        {
            var polX = _context.Poliklinikler.FirstOrDefault(x => x.Id == id);
            if (polX is null)
                return NotFound();
            return polX;
        }

        // POST api/<PoliklinikApiController>
        [HttpPost]
        public IActionResult Post([FromBody] Poliklinik poliklinikFromApi)
        {
                _context.Poliklinikler.Add(poliklinikFromApi);
                _context.SaveChanges();  // Assuming you want to save changes immediately

                return Ok();  // Returning HTTP 200 OK on success
            
        }


        // PUT api/<PoliklinikApiController>/5
        [HttpPut("{id}")]
        public ActionResult<Poliklinik> Put(int id, [FromBody] Poliklinik poliklinikFromApi)
        {
            var existingPoliklinik = _context.Poliklinikler.FirstOrDefault(y => y.Id == id);

            if (existingPoliklinik == null)
            {
                return NotFound(); // Return a 404 response if the entity with the given id is not found
            }

            // Update only the necessary properties
            existingPoliklinik.Adi = poliklinikFromApi.Adi;

            // Save changes to the database
            _context.SaveChanges();

            // Return the updated entity
            return existingPoliklinik;
        }


        // DELETE api/<PoliklinikApiController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var poliklinik = _context.Poliklinikler.FirstOrDefault(x => x.Id == id);

            if (poliklinik == null)
            {
                return NotFound();
            }

            _context.Poliklinikler.Remove(poliklinik);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
