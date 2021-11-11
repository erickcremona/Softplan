using CadastroProdutos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadastroProdutos.Infra.Data.EntityMap
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable(nameof(Produto)).HasKey(p => p.Id);

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(p => p.Categoria)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(p => p.DataCadastro)
                .HasColumnType("date");

            builder.Property(p => p.PrecoCusto)
                .IsRequired()
                .HasColumnType("real");
        }
    }
}
