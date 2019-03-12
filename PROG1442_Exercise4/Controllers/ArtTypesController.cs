using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG1442_Exercise4.Models;

namespace PROG1442_Exercise4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtTypesController : ControllerBase
    {
        private readonly ArtContext _context;

        public ArtTypesController(ArtContext context)
        {
            _context = context;
        }

        // GET: api/ArtTypes
        [HttpGet]
        public IEnumerable<ArtType> GetArtTypes()
        {
            return _context.ArtTypes;
        }

        // GET: api/ArtTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtType([FromRoute] int id)
        {           
            var artType = await _context.ArtTypes.FindAsync(id);

            if (artType == null)
            {
                return NotFound();
            }

            return Ok(artType);
        }

        // PUT: api/ArtTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtType([FromRoute] int id, [FromBody] ArtType artType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artType.ID)
            {
                return BadRequest();
            }

            _context.Entry(artType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtTypeExists(id))
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

        // POST: api/ArtTypes
        [HttpPost]
        public async Task<IActionResult> PostArtType([FromBody] ArtType artType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ArtTypes.Add(artType);
            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetArtType", new { id = artType.ID }, artType);
            }
            catch
            {
                return BadRequest("Unable to save changes to the database. Try again, and if the problem persists see your system administrator.");
            }
        }

        // DELETE: api/ArtTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artType = await _context.ArtTypes.FindAsync(id);
            if (artType == null)
            {
                return BadRequest("Delete Error: Type has already been deleted.");
            }
            try
            {
                _context.ArtTypes.Remove(artType);
                await _context.SaveChangesAsync();

                return Ok(artType);
            }
            catch(DbUpdateException)
            {
                return BadRequest("Unable to save changes to the database. Try again, and if the problem persists see your system administrator.");
            }
        }

        private bool ArtTypeExists(int id)
        {
            return _context.ArtTypes.Any(e => e.ID == id);
        }
    }
}