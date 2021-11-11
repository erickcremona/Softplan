using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Softplan.CalculoPrecoVenda.Api.Extensoes;
using Softplan.CalculoPrecoVenda.Api.MainControllers;
using Softplan.CalculoPrecoVenda.Api.ViewModels;
using Softplan.CalculoPrecoVenda.Domain.Contratos;
using Softplan.CalculoPrecoVenda.Domain.Contratos.Servico;
using Softplan.CalculoPrecoVenda.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softplan.CalculoPrecoVenda.Api.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoriaController : MainController
    {
        private readonly IServicoCategoria _servicoCategoria;
        private readonly IMapper _mapper;

        public CategoriaController(IServicoCategoria servicoCategoria,
                                   INotificador notificador,
                                   IMapper mapper) : base(notificador)
        {          
            _servicoCategoria = servicoCategoria;
            _mapper = mapper;
        }

        [ClaimsAuthorize("Categoria", "ObterPorId")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoriaResponseViewModel>> ObterPorId(Guid id)
        {
            var categoria = await _servicoCategoria.ObterPorIdAsync(id);

            if (categoria == null) return NotFound();

            return _mapper.Map<CategoriaResponseViewModel>(categoria);
        }

        [ClaimsAuthorize("Categoria", "ObterTodos")]
        [HttpGet]
        public async Task<IEnumerable<CategoriaResponseViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<CategoriaResponseViewModel>>(await _servicoCategoria.ObterTodosAsync());
        }

        [ClaimsAuthorize("Categoria", "Adicionar")]
        [HttpPost]
        public async Task<ActionResult<CategoryRequestViewModel>> Adicionar(CategoryRequestViewModel categoryRequestViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _servicoCategoria.AdicionarCategoria(_mapper.Map<Categoria>(categoryRequestViewModel));

            if(result == 1) return CustomResponse(categoryRequestViewModel);

            return CustomResponse();
        }

        [ClaimsAuthorize("Categoria", "Alterar")]
        [HttpPut]
        public async Task<ActionResult<CategoryRequestViewModel>> Alterar(CategoryRequestViewModel categoryRequestViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _servicoCategoria.AlterarCategoria(_mapper.Map<Categoria>(categoryRequestViewModel));

            if (result == 1) return CustomResponse(categoryRequestViewModel);

            return CustomResponse();
        }


        [ClaimsAuthorize("Categoria", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Excluir(Guid id)
        {
            var categoria = await _servicoCategoria.ExcluirCategoria(id);

            if (categoria == null) return NotFound();

            return CustomResponse(_mapper.Map<CategoriaResponseViewModel>(categoria));
        }
    }
}
