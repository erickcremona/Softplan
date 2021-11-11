using Softplan.CalculoPrecoVenda.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Softplan.CalculoPrecoVenda.Domain.Contratos.Repositorio
{
    public interface IRepositorioBase<TEntity> where TEntity : Entity
    {
        void Adicionar(TEntity entity);
        void Alterar(TEntity entity);
        void Excluir(TEntity entity);
        Task<int> SaveChange();
        Task<IEnumerable<TEntity>> ObterTodosAsync();
        Task<TEntity> ObterPorIdAsync(Guid id);
        Task<IEnumerable<TEntity>> ObterPorExprecaoAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
