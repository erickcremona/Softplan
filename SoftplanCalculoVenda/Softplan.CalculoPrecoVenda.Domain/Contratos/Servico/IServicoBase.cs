using FluentValidation;
using FluentValidation.Results;
using Softplan.CalculoPrecoVenda.Domain.Contratos.Repositorio;
using Softplan.CalculoPrecoVenda.Domain.Entidades;

namespace Softplan.CalculoPrecoVenda.Domain.Contratos.Servico
{
    public interface IServicoBase<TEntity>: IRepositorioBase<TEntity> where TEntity : Entity
    {
        void Notificar(ValidationResult validationResult);
        void Notificar(string mensagem);
        bool ExecutarValidacao<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity;
        
    }
}
