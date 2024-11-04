using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Testestefanini.Infra;

namespace Testestefanini.Infrastructure.Maps;

public class PedidoMap : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("Pedido"); 

        builder.HasKey(p => p.Id); 

        builder.Property(p => p.NomeCliente).IsRequired().HasMaxLength(60); 
        builder.Property(p => p.EmailCliente).IsRequired().HasMaxLength(60); 
        builder.Property(p => p.DataCriacao); 
        builder.Property(p => p.Pago); 
        
        builder.HasMany(p => p.Itens)  
            .WithOne(p => p.Pedido)
            .HasForeignKey(e => e.IdPedido)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
