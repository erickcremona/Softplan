using CadastroProdutos.Domain.Entities;
using FluentValidation;

namespace CadastroProdutos.Domain.Validations
{
    public class ValidacaoProduto : AbstractValidator<Produto>
    {
        public ValidacaoProduto()
        {
            RuleFor(d => d.Descricao)
                .NotEmpty().WithMessage("O campo descrição é obrigatório")
                .MaximumLength(50).WithMessage("O tamanho máximo da descrição é de 50 caracteres");

            RuleFor(p => p.PrecoCusto)
                .GreaterThan(0).WithMessage("O preenchimento do preço de custo é obrigatório");

            RuleFor(c => c.Categoria)
                .NotEmpty().WithMessage("O preenchimento da categoria é obrigatório")
                .NotNull().WithMessage("O preenchimento da categoria é obrigatório")
                .MaximumLength(50).WithMessage("O tamanho máximo da categoria é de 50 caracteres"); ;
        }
    }
}
