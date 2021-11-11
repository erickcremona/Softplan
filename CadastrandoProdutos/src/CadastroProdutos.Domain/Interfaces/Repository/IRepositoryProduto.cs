using CadastroProdutos.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroProdutos.Domain.Interfaces.Repository
{
    public interface IRepositoryProduto : IRepositoryBase<Produto>
    {
        Task<List<Produto>> ObterTodosOrdenadosPorDescricao();
    }
}
