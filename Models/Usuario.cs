using System.ComponentModel.DataAnnotations;

namespace PIM_SistemaDeChamados_API.Models
{
    public class Usuario
    {
        [Key] public int IdFunc { get; set; }

        [Required] public string Nome { get; set; } = string.Empty;
        [Required] public string NomeUsuario { get; set; } = string.Empty;

        [Required, EmailAddress] public string Email { get; set; } = string.Empty;

        [Required] public string Senha { get; set; } = string.Empty;

        [Required] public string Funcao { get; set; } = string.Empty; // Admin, Técnico, Usuário
        [Required] public string Setor { get; set; } = string.Empty;   // RH, TI, etc.

        [Required, StringLength(6)] public string Matricula { get; set; } = string.Empty;
    }
}
