using Microsoft.EntityFrameworkCore;
using Testestefanini.Infra;
using Testestefanini.Infrastructure.Maps;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Produto> Produto { get; set; }
    public DbSet<ItensPedido> ItensPedido { get; set; }
    public DbSet<Pedido> Pedido { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ItensPedidoMap());
        modelBuilder.ApplyConfiguration(new ProductMap());
        modelBuilder.ApplyConfiguration(new PedidoMap());
    }
}
