using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pryaniky.Models;

namespace Pryaniky.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PryanikyController : ControllerBase
    {
        private readonly PryanikyContext _context;

        public PryanikyController(PryanikyContext context)
        {
            _context = context;
        }

        // GET: api/PryanikyItems
        [HttpGet]
        public async Task<IEnumerable<Pryanik>> GetPryanikyItems()
        {
            return await _context.Pryaniky.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Pryanik>> PostPryanikyItem(Pryanik pryanik)
        {
            if (pryanik == null)
                return BadRequest(new { Message = "No data" });
            _context.Pryaniky.Add(pryanik);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPryanik),
                new { id = pryanik.Id });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pryanik>> GetPryanik(long id)
        {
            var pryanikyItem = await _context.Pryaniky.FindAsync(id);

            if (pryanikyItem == null)
            {
                return NotFound(new {Message = "Wrong ID"});
            }
            return pryanikyItem;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPryanikyItem(long id, Pryanik pryaniky)
        {
            if (id != pryaniky.Id)
            {
                return BadRequest(new {Message = "Wrong ID"});
            }
            var pryanikyItem = await _context.Pryaniky.FindAsync(id);
            if(pryanikyItem == null)
            {
                return NotFound(new { Message = "Pryanik not found" });
            }
            pryanikyItem.Product = pryaniky.Product;
            pryanikyItem.Price = pryaniky.Price;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!PryanikyItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePryanikyItem(long id)
        {
            var pryanikyItem = await _context.Pryaniky.FindAsync(id);
            if (pryanikyItem == null)
            {
                return NotFound(new {Message = "Pryanik not found"});
            }
            _context.Pryaniky.Remove(pryanikyItem);
            await _context.SaveChangesAsync();
            return Ok(new {Message = "Product deleted"});
        }

        private bool PryanikyItemExists(long id)
        {
            return _context.Pryaniky.Any(e => e.Id == id);
        }
    }
}
