using System;
using System.ComponentModel.DataAnnotations;

namespace Softplan.CalculoPrecoVenda.Api.ViewModels
{
    public class CategoriaResponseViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Nome { get; set; }
        public float Lucro { get; set; }
    }

    public class CategoryRequestViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(50, ErrorMessage = "O Campo Nome deve ter no mínimo {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }
  
    }

    public class CategoryRequestViewModelPrecoVenda
    {
       
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(50, ErrorMessage = "O Campo Nome deve ter no mínimo {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }
                
        public double PrecoCusto { get; set; }
    }
}
