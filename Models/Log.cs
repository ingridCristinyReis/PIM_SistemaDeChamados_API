using System.ComponentModel.DataAnnotations;

namespace PIM_SistemaDeChamados_API.Models
{
    public class Log
    {
        [Key] public int Id { get; set; }
        [Required] public string Usuario { get; set; } = string.Empty;
        [Required] public string Acao { get; set; } = string.Empty;
        [Required] public DateTime DataHora { get; set; }
    }
}
