using System.ComponentModel.DataAnnotations;

namespace PIM_SistemaDeChamados_API.Models
{
    public class Triagem
    {
        [Key]
        public int IdTriagem { get; set; }

        [Required]
        [MaxLength(50)]
        public string Prioridade { get; set; } = "Média";

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Aberto";
    }
}
