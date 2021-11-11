using FluentValidation;
using FluentValidation.Results;
using Softplan.CalculoPrecoVenda.Domain.Contratos;
using Softplan.CalculoPrecoVenda.Domain.Contratos.Repositorio;
using Softplan.CalculoPrecoVenda.Domain.Contratos.Servico;
using Softplan.CalculoPrecoVenda.Domain.Entidades;
using Softplan.CalculoPrecoVenda.Domain.Notificacoes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Softplan.CalculoPrecoVenda.Domain.Servicos
{
    public class ServicoBase<TEntity> : IServicoBase<TEntity> where TEntity : Entity
    {
        private readonly IRepositorioBase<TEntity> _repositorioBase;
        private readonly INotificador _notificador;
        public ServicoBase(IRepositorioBase<TEntity> repositorioBase, INotificador notificador)
        {
            _repositorioBase = repositorioBase;
            _notificador = notificador;
        }

        public void Adicionar(TEntity entity)
        {
            _repositorioBase.Adicionar(entity);
        }

        public void Alterar(TEntity entity)
        {
            _repositorioBase.Alterar(entity);
        }

        public void Excluir(TEntity entity)
        {
           _repositorioBase.Excluir(entity);
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

        public bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade)
            where TV : AbstractValidator<TE>
            where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }

        public async Task<int> SaveChange()
        {
            return await _repositorioBase.SaveChange();
        }

        public async Task<IEnumerable<TEntity>> ObterTodosAsync()
        {
            return await _repositorioBase.ObterTodosAsync();
        }

        public async Task<TEntity> ObterPorIdAsync(Guid id)
        {
            return await _repositorioBase.ObterPorIdAsync(id);
        }

        public async Task<IEnumerable<TEntity>> ObterPorExprecaoAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repositorioBase.ObterPorExprecaoAsync(predicate);
        }
    }
}
