using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM_SistemaDeChamados_API.Models
{
    // Cria a classe Usuario.
    public class Usuario
    {
        [Key] // Para mostrar que idFunc é a chave primária
        public int IdFunc { get; set; } //Cria a coluna idFunc, que corresponde no SQL Server.
        public required string Nome { get; set; } //Cria as propriedades para as colunas, conforme o SQL
        public required string Setor { get; set; } //Cria as propriedades para as colunas, conforme o SQL
        public required string Matricula { get; set; } //Cria as propriedades para as colunas, conforme o SQL
    }
}