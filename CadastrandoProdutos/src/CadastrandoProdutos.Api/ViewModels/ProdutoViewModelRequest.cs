using System;
using System.ComponentModel.DataAnnotations;

namespace CadastrandoProdutos.Api.ViewModels
{
    public class ProdutoViewModelRequest
    {
        [Required(ErrorMessage = "O campo descrição é obrigatório")]
        [MaxLength(50, ErrorMessage = "O tamanho máximo da descrição é de 50 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O preenchimento do preço de custo é obrigatório")]
        [Range(0.1, double.MaxValue, ErrorMessage = "O preenchimento do preço de custo deve ser maior que 0")]
        public double PrecoCusto { get; set; }

        [Required(ErrorMessage = "O campo categoria é obrigatório")]
        [MaxLength(50, ErrorMessage = "O preenchimento da categoria é obrigatório")]
        public string Categoria { get; set; }
    }
}
