using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM_SistemaDeChamados_API.Models
{
    public class Tecnico // Cria a classe
    {
        [Key] //Para mostrar que IdFun é a chave primária
        public int IdTecnico { get; set; }

        //Cria as propriedades para as colunas, conforme o SQL
        public required string Especialidade { get; set; }
        public bool Disponibilidade { get; set; } // BIT no SQL Server é um tipo booleano em C#
        
        [ForeignKey("IdFunc")]
        public required  Usuario Usuario { get; set; }
    }
}