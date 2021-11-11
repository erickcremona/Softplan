using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastrandoProdutos.Api.ViewModels
{
    public class ProdutoViewModelResponse
    {
        public Guid Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Descricao { get; set; }
        public double PrecoCusto { get; set; }
        public string Categoria { get; set; }
        public double PrecoVenda { get; set; }
    }
}
