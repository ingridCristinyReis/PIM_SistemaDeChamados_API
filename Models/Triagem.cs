using System.ComponentModel.DataAnnotations;

namespace PIM_SistemaDeChamados_API.Models
{
    public class Triagem // Cria a classe
    {
        [Key] //Para mostrar que IdFun é a chave primária
        public int IdTriagem { get; set; }

         //Cria as propriedades para as colunas, conforme o SQL
        public required string Prioridade { get; set; }
        public required string Status { get; set; }
    }
}