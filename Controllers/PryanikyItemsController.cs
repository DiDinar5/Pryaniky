using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<IEnumerable<PryanikyItemDTO>>> GetPryanikyItems()
        {
            return await _context.PryanikyItems.Select(x=> ItemPryanikyDTO(x)).ToListAsync();
        }

        // GET: api/PryanikyItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PryanikyItemDTO>> GetPryanikyItem(long id)
        {
            var pryanikyItem = await _context.PryanikyItems.FindAsync(id);

            if (pryanikyItem == null)
            {
                return NotFound();
            }
            return ItemPryanikyDTO(pryanikyItem);
        }

        // PUT: api/PryanikyItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPryanikyItem(long id, PryanikyItemDTO pryanikyDTO)
        {
            if (id != pryanikyDTO.Id)
            {
                return BadRequest();
            }
            var pryanikyItem = await _context.PryanikyItems.FindAsync(id);
            if(pryanikyItem == null)
            {
                return NotFound();
            }
            pryanikyItem.Name = pryanikyDTO.Name;
            pryanikyItem.IsComplete = pryanikyDTO.IsComplete;
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

        // POST: api/PryanikyItems
        [HttpPost]
        public async Task<ActionResult<PryanikyItemDTO>> PostPryanikyItem(PryanikyItemDTO pryanikyDTO)
        {
            var pryanikyItem = new PryanikyItem
            {
                IsComplete = pryanikyDTO.IsComplete,
                Name = pryanikyDTO.Name
            };
            _context.PryanikyItems.Add(pryanikyItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPryanikyItem),
                new { id = pryanikyItem.Id },
                ItemPryanikyDTO(pryanikyItem));
        }

        // DELETE: api/PryanikyItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePryanikyItem(long id)
        {
            var pryanikyItem = await _context.PryanikyItems.FindAsync(id);
            if (pryanikyItem == null)
            {
                return NotFound();
            }
            _context.PryanikyItems.Remove(pryanikyItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool PryanikyItemExists(long id)
        {
            return _context.PryanikyItems.Any(e => e.Id == id);
        }
        private static PryanikyItemDTO ItemPryanikyDTO(PryanikyItem item) =>
            new PryanikyItemDTO()
            {
                Id= item.Id,
                Name= item.Name,    
                IsComplete= item.IsComplete,
            };
    }
}
