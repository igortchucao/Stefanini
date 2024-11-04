using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Testestefanini.Infra;

public class ItensPedidoMap : IEntityTypeConfiguration<ItensPedido>
{
    public void Configure(EntityTypeBuilder<ItensPedido> builder)
    {

        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.Pedido)
            .WithMany(p => p.Itens)  
            .HasForeignKey(e => e.IdPedido)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Produto)
            .WithMany()
            .HasForeignKey(e => e.IdProduto)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.Quantidade)
            .IsRequired();
    }
}
