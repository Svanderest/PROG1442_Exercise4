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
    public class ArtworksController : ControllerBase
    {
        private readonly ArtContext _context;

        public ArtworksController(ArtContext context)
        {
            _context = context;
        }

        // GET: api/Artworks
        [HttpGet]
        public IEnumerable<Artwork> GetArtworks()
        {
            return _context.Artworks.Include(a => a.ArtType);
        }

        // GET: api/PatientsByType
        [HttpGet("ByType/{id}")]
        public IEnumerable<Artwork> GetArtworkByType(int id)
        {
            return _context.Artworks.Include(a => a.ArtType)
                .Where(a => a.ArtTypeID == id);
        }

        // GET: api/Artworks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtwork([FromRoute] int id)
        {
            var artwork = await _context.Artworks
                .Where(a => a.ID == id)
                .Include(a => a.ArtType)
                .SingleOrDefaultAsync();

            if (artwork == null)
            {
                return NotFound();
            }

            return Ok(artwork);
        }

        // PUT: api/Artworks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtwork([FromRoute] int id, [FromBody] Artwork artwork)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artwork.ID)
            {
                return BadRequest();
            }

            _context.Entry(artwork).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(artwork);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtworkExists(id))
                {
                    ModelState.AddModelError("", "Concurrency Error: Artwork has been Removed.");
                    return BadRequest(ModelState);
                }
                else
                {
                    ModelState.AddModelError("", "Concurrency Error: Artowrk has been updated by another user.  Cancel and try editing the record again.");
                    return BadRequest(ModelState);
                }
            }
            catch (DbUpdateException dex)
            {
                if (dex.InnerException.Message.Contains("IX_"))
                {
                    ModelState.AddModelError("", "Unable to save changes: Duplicate Name for works of this type.");
                    return BadRequest(ModelState);
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes to the database. Try again, and if the problem persists see your system administrator.");
                    return BadRequest(ModelState);
                }
            }
        }

        // POST: api/Artworks
        [HttpPost]
        public async Task<IActionResult> PostArtwork([FromBody] Artwork artwork)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Artworks.Add(artwork);
            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetArtworks", new { id = artwork.ID }, artwork);
            }
            catch (DbUpdateException dex)
            {
                if (dex.InnerException.Message.Contains("IX_"))
                {
                    ModelState.AddModelError("", "Unable to save: Duplicate Name for works of this type.");
                    return BadRequest(ModelState);
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes to the database. Try again, and if the problem persists see your system administrator.");
                    return BadRequest(ModelState);
                }
            }
        }

        // DELETE: api/Artworks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtwork([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artwork = await _context.Artworks.FindAsync(id);
            if (artwork == null)
            {
                ModelState.AddModelError("", "Delete Error: Artwork has already been Removed.");
            }
            else
            {
                try
                {
                    _context.Artworks.Remove(artwork);
                    await _context.SaveChangesAsync();
                    return Ok(artwork);
                }
                catch
                {
                    ModelState.AddModelError("", "Delete Error: Unable to delete Artwork.");
                }
            }
            return BadRequest(ModelState);
        }

        private bool ArtworkExists(int id)
        {
            return _context.Artworks.Any(e => e.ID == id);
        }
    }
}