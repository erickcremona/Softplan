using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Softplan.CalculoPrecoVenda.Domain.Entidades;

namespace Softplan.CalculoPrecoVenda.Data.EntityMap
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable(nameof(Categoria)).HasKey(c => c.Id);

            builder.Property(p => p.Nome)
               .HasColumnType("varchar(80)")
               .IsRequired();

            builder.Property(p => p.Lucro)
              .HasColumnType("float")
              .IsRequired();
        }
    }
}
