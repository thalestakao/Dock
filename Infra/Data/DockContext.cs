using Dock.Domain.Entities.Conta;
using Dock.Domain.Entities.Cliente;
using Microsoft.EntityFrameworkCore;
using Dock.Infra.Data.Mappings;

namespace Dock.Infra.Data
{
    public class DockContext : DbContext
    {
        public DbSet<ContaDigital> Contas { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<Portador> Portadores { get; set; }
        //public DockContext(DbContextOptions<DockContext> options) : base(options)
        //{ 
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Dock;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True;ConnectRetryCount=3;ConnectRetryInterval=10");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContaMap());
            modelBuilder.ApplyConfiguration(new TransacaoMap());
            modelBuilder.ApplyConfiguration(new PortadorMap());
        }


    }
}
