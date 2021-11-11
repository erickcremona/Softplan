using Microsoft.EntityFrameworkCore;
using Softplan.CalculoPrecoVenda.Data.Contexto;
using Softplan.CalculoPrecoVenda.Domain.Contratos.Repositorio;
using Softplan.CalculoPrecoVenda.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Softplan.CalculoPrecoVenda.Data.Repositorio
{
    public class RepositorioBase<TEntity> : IRepositorioBase<TEntity> where TEntity : Entity
    {
        private readonly SoftplanDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        public RepositorioBase(SoftplanDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public void Adicionar(TEntity entity)
            => _dbSet.Add(entity);        

        public void Alterar(TEntity entity)
            => _dbSet.Update(entity);        

        public void Excluir(TEntity entity)
            => _dbSet.Remove(entity);        

        public async Task<IEnumerable<TEntity>> ObterPorExprecaoAsync(Expression<Func<TEntity, bool>> predicate)
            => await _dbSet.AsNoTracking().Where(predicate).ToListAsync();

        public async Task<TEntity> ObterPorIdAsync(Guid id)
            => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<TEntity>> ObterTodosAsync()
            => await _dbSet.ToListAsync();

        public async Task<int> SaveChange()
            => await _context.SaveChangesAsync();
    }
}
