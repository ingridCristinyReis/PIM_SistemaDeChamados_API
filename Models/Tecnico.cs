using System.ComponentModel.DataAnnotations;

namespace PIM_SistemaDeChamados_API.Models
{
    public class Tecnico
    {
        [Key]
        public int IdTecnico { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string Especialidade { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        public int IdFunc { get; set; }  // FK para o Usuario
    }
}
