using CadastroProdutos.Domain.Entities;
using CadastroProdutos.Domain.Interfaces.Repository;
using CadastroProdutos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CadastroProdutos.Infra.Data.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : Entity 
    {
        private readonly CadastroProdutosContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(CadastroProdutosContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public void Adicionar(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Alterar(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Excluir(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> ObterPorExprecaoAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking()
                .Where(predicate).ToListAsync();
        }

        public async Task<TEntity> ObterPorIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> ObterTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
