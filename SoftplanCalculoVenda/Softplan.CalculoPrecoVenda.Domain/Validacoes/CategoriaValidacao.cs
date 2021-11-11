using FluentValidation;
using Softplan.CalculoPrecoVenda.Domain.Entidades;

namespace Softplan.CalculoPrecoVenda.Domain.Validacoes
{
    public class CategoriaValidacao : AbstractValidator<Categoria>
    {
        public CategoriaValidacao()
        {
            RuleFor(n => n.Nome)
               .NotEmpty().WithMessage("O preenchimento da descrição é obrigatório")
               .Length(2, 100)
               .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
            
        }
    }
}
