using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroProdutos.Domain.Interfaces.Service
{
    public interface IHttpService
    {
        Task<double> ObterPrecoVenda(double precoCusto, string categoria);
    }
}
