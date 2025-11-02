using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIM_SistemaDeChamados_API.Data;
using PIM_SistemaDeChamados_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace PIM_SistemaDeChamados_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriagensController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TriagensController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Triagem>>> GetTriagens()
        {
            return await _context.Triagens.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Triagem>> GetTriagem(int id)
        {
            var triagem = await _context.Triagens.FindAsync(id);

            if (triagem == null)
            {
                return NotFound();
            }

            return triagem;
        }

        [HttpPost]
        public async Task<ActionResult<Triagem>> PostTriagem(Triagem triagem)
        {
            _context.Triagens.Add(triagem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTriagem), new { id = triagem.IdTriagem }, triagem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTriagem(int id, Triagem triagem)
        {
            if (id != triagem.IdTriagem)
            {
                return BadRequest();
            }

            _context.Entry(triagem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TriagemExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTriagem(int id)
        {
            var triagem = await _context.Triagens.FindAsync(id);
            if (triagem == null)
            {
                return NotFound();
            }

            _context.Triagens.Remove(triagem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TriagemExists(int id)
        {
            return _context.Triagens.Any(e => e.IdTriagem == id);
        }
    }
}