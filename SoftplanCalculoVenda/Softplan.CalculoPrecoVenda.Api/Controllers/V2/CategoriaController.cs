using Microsoft.AspNetCore.Mvc;
using Softplan.CalculoPrecoVenda.Api.MainControllers;
using Softplan.CalculoPrecoVenda.Api.ViewModels;
using Softplan.CalculoPrecoVenda.Domain.Contratos;
using Softplan.CalculoPrecoVenda.Domain.Contratos.Servico;
using System.Threading.Tasks;

namespace Softplan.CalculoPrecoVenda.Api.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoriaController : MainController
    {
        private readonly IServicoCategoria _servicoCategoria;
        private readonly INotificador _notificador;
        public CategoriaController(IServicoCategoria servicoCategoria,
                                   INotificador notificador) : base(notificador)
        {
            _servicoCategoria = servicoCategoria;
            _notificador = notificador;
        }

        [HttpGet("calcular-preco")]
        public async Task<ActionResult<double>> CalcularPrecoVenda([FromQuery] string categoria, [FromQuery] string preco)
        {
            double precoVenda = 0;

            if(string.IsNullOrEmpty(preco)) return CustomResponse(0);

            preco = string.Format("{0:n2}", preco.Replace('.', ','));

            if (double.TryParse(preco, out precoVenda) && precoVenda > 0)
            {               
                var categoriaResult = await _servicoCategoria.ObterCategoriaPorNome(categoria);

                if (categoriaResult == null) NotificarErro("Categoria informada não foi localizada");

                if (!_notificador.TemNotificacao())
                    return CustomResponse(_servicoCategoria.CalcularPrecoVenda(categoriaResult, precoVenda));
            }
                       
            return CustomResponse(0);
        }

        [HttpPost("calcular")]
        public async Task<ActionResult<double>> CalcularPrecoVenda([FromBody] CategoryRequestViewModelPrecoVenda categoria)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (categoria.PrecoCusto <= 0)
            {
                categoria.PrecoCusto = 0;
                return CustomResponse(0);
            }

            var categoriaResult = await _servicoCategoria.ObterCategoriaPorNome(categoria.Nome);

            if (categoriaResult == null) NotificarErro("Categoria informada não foi localizada");

            if (!_notificador.TemNotificacao())
                return CustomResponse(_servicoCategoria.CalcularPrecoVenda(categoriaResult, categoria.PrecoCusto));

            return CustomResponse();
        }
    }
}
