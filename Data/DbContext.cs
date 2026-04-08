using Locadora.Models;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Data
{
    

    public class LocadoraContext : DbContext
    {
        public LocadoraContext(DbContextOptions<LocadoraContext> options) : base(options)
        {
        }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Fabricante> Fabricantes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Aluguel> Alugueis { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
           
            _=options.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Locadora;Trusted_Connection=True;TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluguel>()
                .Property(a => a.ValorDiaria)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Aluguel>()
                .Property(a => a.ValorTotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Categoria>()
                .Property(c => c.ValorDiariaPadrao)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.CPF)
                .IsUnique();

        }
    }
}
