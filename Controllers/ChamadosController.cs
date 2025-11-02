using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIM_SistemaDeChamados_API.Data;
using PIM_SistemaDeChamados_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIM_SistemaDeChamados_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChamadosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChamadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chamado>>> GetChamados()
        {
            return await _context.Chamados.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Chamado>> GetChamado(int id)
        {
            var chamado = await _context.Chamados.FindAsync(id);

            if (chamado == null)
            {
                return NotFound();
            }

            return chamado;
        }

        [HttpPost]
        public async Task<ActionResult<Chamado>> PostChamado(Chamado chamado)
        {
            _context.Chamados.Add(chamado);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChamado), new { id = chamado.IdChamado }, chamado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutChamado(int id, Chamado chamado)
        {
            if (id != chamado.IdChamado)
                return BadRequest();

            var chamadoExistente = await _context.Chamados.FindAsync(id);
            if (chamadoExistente == null)
                return NotFound();

            // Atualiza os campos
            chamadoExistente.Titulo = chamado.Titulo;
            chamadoExistente.Descricao = chamado.Descricao;
            chamadoExistente.Prioridade = chamado.Prioridade;
            chamadoExistente.Status = chamado.Status;
            chamadoExistente.Resolucao = chamado.Resolucao;

            // Se o técnico marcar como fechado ou concluído
            if (chamado.Status == "Fechado" || chamado.Status == "Concluído")
                chamadoExistente.DataFechamento = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChamadoExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChamado(int id)
        {
            var chamado = await _context.Chamados.FindAsync(id);
            if (chamado == null)
            {
                return NotFound();
            }

            _context.Chamados.Remove(chamado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChamadoExists(int id)
        {
            return _context.Chamados.Any(e => e.IdChamado == id);
        }
    }
}