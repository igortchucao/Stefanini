using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Testestefanini.Infra;

namespace Testestefanini.Infrastructure.Maps;

public class ProductMap : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produto"); 

        builder.HasKey(p => p.Id); 

        builder.Property(p => p.NomeProduto).IsRequired().HasMaxLength(20); 
        builder.Property(p => p.Valor).HasColumnType("decimal(10,2)"); 
    }
}