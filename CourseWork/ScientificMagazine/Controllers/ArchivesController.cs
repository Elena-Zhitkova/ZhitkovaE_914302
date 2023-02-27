using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScientificMagazine.Data;
using ScientificMagazine.Models;

namespace ScientificMagazine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArchivesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Archives
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Archive>>> GetArchives()
        {
            return await _context.Archives.ToListAsync();
        }

        // GET: api/Archives/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Archive>> GetArchive(int id)
        {
            var archive = await _context.Archives.FindAsync(id);

            if (archive == null)
            {
                return NotFound();
            }

            return archive;
        }

        // PUT: api/Archives/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArchive(int id, Archive archive)
        {
            if (id != archive.Id)
            {
                return BadRequest();
            }

            _context.Entry(archive).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArchiveExists(id))
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

        // POST: api/Archives
        [HttpPost]
        public async Task<ActionResult<Archive>> PostArchive(Archive archive)
        {
            _context.Archives.Add(archive);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArchive", new { id = archive.Id }, archive);
        }

        // DELETE: api/Archives/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArchive(int id)
        {
            var archive = await _context.Archives.FindAsync(id);
            if (archive == null)
            {
                return NotFound();
            }

            _context.Archives.Remove(archive);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArchiveExists(int id)
        {
            return _context.Archives.Any(e => e.Id == id);
        }
    }
}
