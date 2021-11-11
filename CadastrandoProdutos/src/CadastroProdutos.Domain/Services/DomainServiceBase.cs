using CadastroProdutos.Domain.Entities;
using CadastroProdutos.Domain.Interfaces;
using CadastroProdutos.Domain.Interfaces.Repository;
using CadastroProdutos.Domain.Interfaces.Service;
using CadastroProdutos.Domain.Notifications;
using FluentValidation;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace CadastroProdutos.Domain.Services
{
    public class DomainServiceBase<TEntity> : IDomainServiceBase<TEntity> where TEntity : Entity
    {
        private readonly IRepositoryBase<TEntity> _repositoryBase;
        protected readonly INotificador _notificador;

        public DomainServiceBase(IRepositoryBase<TEntity> repositoryBase, INotificador notificador)
        {
            _repositoryBase = repositoryBase;
            _notificador = notificador;
        }
        public void Adicionar(TEntity entity)
        {
            _repositoryBase.Adicionar(entity);
        }

        public void Alterar(TEntity entity)
        {
            _repositoryBase.Alterar(entity);
        }

        public void Dispose()
        {
            _repositoryBase.Dispose();
        }

        public void Excluir(TEntity entity)
        {
            _repositoryBase.Excluir(entity);
        }

        public bool ExecutarValicacao<TV, TE>(TV validation, TE entity)
            where TV : AbstractValidator<TE>
            where TE : Entity
        {
            var validar = validation.Validate(entity);

            if (validar.IsValid) return true;

            Notificar(validar);

            return false;
        }

        public void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        public void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        public async Task<int> SaveChanges()
        {
            return await _repositoryBase.SaveChanges();
        }
    }
}
