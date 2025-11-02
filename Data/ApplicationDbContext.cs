using Microsoft.EntityFrameworkCore;
using PIM_SistemaDeChamados_API.Models;

namespace PIM_SistemaDeChamados_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Tecnico> Tecnicos => Set<Tecnico>();
        public DbSet<Chamado> Chamados => Set<Chamado>();
        public DbSet<Triagem> Triagens => Set<Triagem>();
        public DbSet<Log> Logs => Set<Log>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SistemaDeChamados;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            }
        }

    }
}
