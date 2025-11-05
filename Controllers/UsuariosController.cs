using Microsoft.AspNetCore.Identity;
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
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (model is null || string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Senha))
                return BadRequest(new { message = "Email ou senha inválidos." });

            // 🔹 Agora busca pelo EMAIL
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user is null)
                return Unauthorized(new { message = "Email ou senha incorretos." });

            var senhaDb = user.Senha ?? string.Empty;

            // 🔐 bcrypt (caso tenha senhas criptografadas)
            if (senhaDb.StartsWith("$2a$") || senhaDb.StartsWith("$2b$") || senhaDb.StartsWith("$2y$"))
            {
                if (BCrypt.Net.BCrypt.Verify(model.Senha, senhaDb))
                    return Ok(new
                    {
                        message = "Login OK (bcrypt)",
                        user.IdFunc,
                        user.NomeUsuario,
                        user.Email,
                        user.Funcao
                    });

                return Unauthorized(new { message = "Email ou senha incorretos." });
            }

            // 🔑 Identity (PasswordHasher)
            try
            {
                var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<Usuario>();
                var res = hasher.VerifyHashedPassword(user, senhaDb, model.Senha);
                if (res != Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed)
                    return Ok(new
                    {
                        message = "Login OK (PasswordHasher)",
                        user.IdFunc,
                        user.NomeUsuario,
                        user.Email,
                        user.Funcao
                    });
            }
            catch { }

            // 🧩 Fallback: texto puro (para registros antigos sem hash)
            if (senhaDb == model.Senha)
                return Ok(new
                {
                    message = "Login OK (plaintext)",
                    user.IdFunc,
                    user.NomeUsuario,
                    user.Email,
                    user.Funcao
                });

            return Unauthorized(new { message = "Email ou senha incorretos." });
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
