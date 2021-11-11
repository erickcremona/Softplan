using CadastroProdutos.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace CadastroProdutos.Domain.Interfaces.Service
{
    public interface IDomainServiceBase<TEntity>: IDisposable where TEntity : Entity
    {
        void Adicionar(TEntity entity);
        void Alterar(TEntity entity);
        void Excluir(TEntity entity);
        Task<int> SaveChanges();
        void Notificar(ValidationResult validationResult);
        void Notificar(string mensagem);
        bool ExecutarValicacao<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity; 
    }
}
