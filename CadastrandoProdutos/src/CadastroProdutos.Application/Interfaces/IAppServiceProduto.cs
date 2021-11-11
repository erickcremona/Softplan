using CadastroProdutos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroProdutos.Application.Interfaces
{
    public interface IAppServiceProduto
    {
        Task Adicionar(Produto produto);
        Task Alterar(Produto produto);
        Task Excluir(Guid Id);
        Task<IEnumerable<Produto>> ObterTodos();
    }
}
