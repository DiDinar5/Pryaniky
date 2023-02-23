using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pryaniky.Models;

namespace Pryaniky.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PryanikyItemsController : ControllerBase
    {
        private readonly PryanikyContext _context;

        public PryanikyItemsController(PryanikyContext context)
        {
            _context = context;
        }

        // GET: api/PryanikyItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PryanikyItem>>> GetPryanikyItems()
        {
            return await _context.PryanikyItemContext.Select(x=> ItemPryaniky(x)).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<PryanikyItem>> PostPryanikyItem(PryanikyItem pryaniky)
        {
            var pryanikyItem = new PryanikyItem
            {
                Product = pryaniky.Product,
                Price = pryaniky.Price
            };
            _context.PryanikyItemContext.Add(pryanikyItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPryanik),
                new { id = pryanikyItem.Id },
                ItemPryaniky(pryanikyItem));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PryanikyItem>> GetPryanik(long id)
        {
            var pryanikyItem = await _context.PryanikyItemContext.FindAsync(id);

            if (pryanikyItem == null)
            {
                return NotFound(new {Message = "неверный ID"});
            }
            return ItemPryaniky(pryanikyItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPryanikyItem(long id, PryanikyItem pryaniky)
        {
            if (id != pryaniky.Id)
            {
                return BadRequest(new {Message = "ID не найден"});
            }
            var pryanikyItem = await _context.PryanikyItemContext.FindAsync(id);
            if(pryanikyItem == null)
            {
                return NotFound(new { Message = "ID равен null" });
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
            var pryanikyItem = await _context.PryanikyItemContext.FindAsync(id);
            if (pryanikyItem == null)
            {
                return NotFound(new {Message = "ID не найден"});
            }
            _context.PryanikyItemContext.Remove(pryanikyItem);
            await _context.SaveChangesAsync();
            return Ok(new {Message = "Product deleted"});
        }

        private bool PryanikyItemExists(long id)
        {
            return _context.PryanikyItemContext.Any(e => e.Id == id);
        }
        private static PryanikyItem ItemPryaniky(PryanikyItem item) =>
            new PryanikyItem()
            {
                Id= item.Id,
                Product= item.Product,    
                Price= item.Price,
            };
    }
}
