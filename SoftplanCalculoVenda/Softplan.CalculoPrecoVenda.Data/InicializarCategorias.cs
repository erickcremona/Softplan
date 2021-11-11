using Microsoft.EntityFrameworkCore;
using Softplan.CalculoPrecoVenda.Domain.Entidades;
using System;

namespace Softplan.CalculoPrecoVenda.Data
{
    public static class InicializarCategorias
    {
        public static void Inicializar(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria{ Id = Guid.NewGuid(), DataCriacao = DateTime.Now.Date, Nome = "Brinquedos", Lucro = 25f },
                 new Categoria { Id = Guid.NewGuid(), DataCriacao = DateTime.Now.Date, Nome = "Bebidas", Lucro = 30f },
                  new Categoria { Id = Guid.NewGuid(), DataCriacao = DateTime.Now.Date, Nome = "Informática", Lucro = 10f },
                   new Categoria { Id = Guid.NewGuid(), DataCriacao = DateTime.Now.Date, Nome = "Softplan", Lucro = 5f }
            );
        }
    }
}
