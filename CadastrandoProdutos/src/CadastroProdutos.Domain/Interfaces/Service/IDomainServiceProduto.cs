using CadastroProdutos.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace CadastroProdutos.Domain.Interfaces.Service
{
    public interface IDomainServiceProduto : IDomainServiceBase<Produto>
    {
        bool Validar(Produto produto);
        Task AdicionarProduto(Produto produto);
        Task AlterarProduto(Produto produto);
        Task ExcluirProduto(Guid id);
    }
}
