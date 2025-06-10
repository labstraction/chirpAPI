using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using chirpAPI.model;

namespace chirpAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChirpsController : ControllerBase
    {
        private readonly CinguettioContext _context;

        public ChirpsController(CinguettioContext context)
        {
            _context = context;
        }

        // GET: api/Chirps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chirp>>> GetChirpsByFilter([FromQuery] ChirpFilter filter)
        {
            IQueryable<Chirp> query = _context.Chirps.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Text))
            {
                query = query.Where(x => x.Text == filter.Text);
            }

            var result = await query.ToListAsync();

            if (result == null || !result.Any())
            {
                return NoContent();
            }
            else
            {
                return Ok(result);
            }
        }

        // GET: api/Chirps
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Chirp>>> GetAllChirps()
        {
            return await _context.Chirps.ToListAsync();
        }

        // GET: api/Chirps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chirp>> GetChirp([FromRoute]int id)
        {
            var chirp = await _context.Chirps.FindAsync(id);

            if (chirp == null)
            {
                return NotFound();
            }

            return chirp;
        }

        // PUT: api/Chirps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChirp([FromRoute]int id, [FromBody] Chirp chirp)
        {
            if (id != chirp.Id)
            {
                return BadRequest("id diversi");
            }

            _context.Entry(chirp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChirpExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Chirps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Chirp>> PostChirp([FromBody]Chirp chirp)
        {
            _context.Chirps.Add(chirp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChirp", new { id = chirp.Id }, chirp);
        }

        // DELETE: api/Chirps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChirp([FromRoute] int id)
        {
            var chirp = await _context.Chirps.FindAsync(id);
            if (chirp == null)
            {
                return NotFound();
            }

            _context.Chirps.Remove(chirp);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChirpExists(int id)
        {
            return _context.Chirps.Any(e => e.Id == id);
        }
    }
}
