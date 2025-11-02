using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIM_SistemaDeChamados_API.Data;
using PIM_SistemaDeChamados_API.Models;

namespace PIM_SistemaDeChamados_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UsuariosController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
            => await _context.Usuarios.AsNoTracking().ToListAsync();

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario>> GetOne(int id)
        {
            var u = await _context.Usuarios.FindAsync(id);
            return u is null ? NotFound() : u;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(Usuario usuario)
        {
            // força de senha (LGPD)
            var ok = System.Text.RegularExpressions.Regex.IsMatch(
                usuario.Senha ?? "",
                @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$");
            if (!ok) return BadRequest("A senha deve conter 8+ caracteres, com maiúscula, minúscula, número e símbolo.");

            // hash
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // log
            _context.Logs.Add(new Log
            {
                Usuario = usuario.NomeUsuario,
                Acao = $"Usuário {usuario.NomeUsuario} cadastrado.",
                DataHora = DateTime.Now
            });
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOne), new { id = usuario.IdFunc }, usuario);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario input)
        {
            if (input is null || string.IsNullOrWhiteSpace(input.NomeUsuario) || string.IsNullOrWhiteSpace(input.Senha))
                return BadRequest("Usuário ou senha inválidos.");

            var user = await _context.Usuarios.FirstOrDefaultAsync(x => x.NomeUsuario == input.NomeUsuario);
            if (user is null || !BCrypt.Net.BCrypt.Verify(input.Senha, user.Senha))
                return Unauthorized("Usuário ou senha incorretos.");

            return Ok(new { mensagem = "Login ok", user.IdFunc, user.NomeUsuario, user.Email, user.Funcao });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var u = await _context.Usuarios.FindAsync(id);
            if (u is null) return NotFound();
            _context.Usuarios.Remove(u);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
