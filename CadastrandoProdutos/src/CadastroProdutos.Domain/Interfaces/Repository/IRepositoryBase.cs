using CadastroProdutos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CadastroProdutos.Domain.Interfaces.Repository
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : Entity
    {
        void Adicionar(TEntity entity);
        void Alterar(TEntity entity);
        void Excluir(TEntity entity);
        Task<int> SaveChanges();
        Task<IEnumerable<TEntity>> ObterTodosAsync();
        Task<TEntity> ObterPorIdAsync(Guid id);
        Task<IEnumerable<TEntity>> ObterPorExprecaoAsync(Expression<Func<TEntity, bool>> predicate);       
    }
}
