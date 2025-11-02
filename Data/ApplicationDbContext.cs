using Microsoft.EntityFrameworkCore;
using PIM_SistemaDeChamados_API.Models;

namespace PIM_SistemaDeChamados_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        // DbSets representam as tabelas no banco de dados
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Tecnico> Tecnicos { get; set; } = null!;
        public DbSet<Chamado> Chamados { get; set; } = null!;
        public DbSet<Triagem> Triagens { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura relacionamento entre Tecnico e Usuario
            modelBuilder.Entity<Tecnico>()
                .HasOne(t => t.Usuario)
                .WithMany()
                .HasForeignKey("IdFunc")
                .IsRequired();
            
            // Configura propriedades obrigatórias como required
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Nome)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Setor)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Matricula)
                .IsRequired();

            modelBuilder.Entity<Tecnico>()
                .Property(t => t.Especialidade)
                .IsRequired();

            modelBuilder.Entity<Chamado>()
                .Property(c => c.Descricao)
                .IsRequired();

            modelBuilder.Entity<Triagem>()
                .Property(t => t.Prioridade)
                .IsRequired();

            modelBuilder.Entity<Triagem>()
                .Property(t => t.Status)
                .IsRequired();
        }
    }
}
