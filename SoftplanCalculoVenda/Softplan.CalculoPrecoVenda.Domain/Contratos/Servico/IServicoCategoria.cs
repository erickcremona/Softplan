using Softplan.CalculoPrecoVenda.Domain.Entidades;
using System;
using System.Threading.Tasks;

namespace Softplan.CalculoPrecoVenda.Domain.Contratos.Servico
{
    public interface IServicoCategoria : IServicoBase<Categoria>
    {
        double CalcularPrecoVenda(Categoria categoria, double precoCusto);
        public bool Validar(Categoria categoria);
        public Task<int> AdicionarCategoria(Categoria categoria);
        public Task<int> AlterarCategoria(Categoria categoria);
        public Task<Categoria> ExcluirCategoria(Guid id);
        public Task<Categoria> ObterCategoriaPorNome(string nomeCategoria);
    }
}
