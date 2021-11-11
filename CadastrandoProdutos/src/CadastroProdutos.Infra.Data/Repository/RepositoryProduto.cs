using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroProdutos.Domain.Entities;
using CadastroProdutos.Domain.Interfaces.Repository;
using CadastroProdutos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CadastroProdutos.Infra.Data.Repository
{    
    public class RepositoryProduto: RepositoryBase<Produto>, IRepositoryProduto
    {   

        public RepositoryProduto(CadastroProdutosContext context) : base (context)
        {

        }

        public async Task<List<Produto>> ObterTodosOrdenadosPorDescricao()
        {
            return await _dbSet.AsNoTracking().OrderBy(p => p.Descricao).ToListAsync();
        }
    }
}
