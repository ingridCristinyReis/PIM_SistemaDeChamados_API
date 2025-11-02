using System;
using System.ComponentModel.DataAnnotations;

namespace PIM_SistemaDeChamados_API.Models
{
    public class Chamado
    {
        [Key]
        public int IdChamado { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public string? Status { get; set; }
    }
}