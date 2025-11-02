using System;
using System.ComponentModel.DataAnnotations;

namespace PIM_SistemaDeChamados_API.Models
{
    public class Chamado
    {
        [Key]
        public int IdChamado { get; set; }

        [Required]
        [MaxLength(150)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Prioridade { get; set; } = "Média";

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Aberto";

        [Required]
        public DateTime DataAbertura { get; set; } = DateTime.Now;

        [MaxLength(500)]
        public string? Resolucao { get; set; }

        public DateTime? DataFechamento { get; set; }
    }
}
