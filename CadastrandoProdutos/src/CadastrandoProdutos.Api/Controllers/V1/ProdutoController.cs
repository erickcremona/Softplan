using AutoMapper;
using CadastrandoProdutos.Api.Extensions;
using CadastrandoProdutos.Api.Main;
using CadastrandoProdutos.Api.ViewModels;
using CadastroProdutos.Application.Interfaces;
using CadastroProdutos.Domain.Entities;
using CadastroProdutos.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastrandoProdutos.Api.Controllers.V1
{

    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProdutoController : MainController
    {
        private readonly IAppServiceProduto _appServiceProduto;
        private readonly IMapper _mapper;
        public ProdutoController(INotificador notificador, 
                                 IAppServiceProduto appServiceProduto,
                                 IMapper mapper) : base(notificador)
        {
            _appServiceProduto = appServiceProduto;
            _mapper = mapper;
        }

        [ClaimsAuthorize("Produto", "Adicionar")]
        [HttpPost]
        public async Task<ActionResult> Adicionar([FromBody] ProdutoViewModelRequest produtoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _appServiceProduto.Adicionar(_mapper.Map<Produto>(produtoViewModel));

            return CustomResponse(produtoViewModel);
        }

        [ClaimsAuthorize("Produto", "Alterar")]
        [HttpPut]
        public async Task<ActionResult> Alterar(ProdutoViewModelRequest produtoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _appServiceProduto.Alterar(_mapper.Map<Produto>(produtoViewModel));

            return CustomResponse(produtoViewModel);
        }

        [ClaimsAuthorize("Produto", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Excluir(Guid id)
        {
            await _appServiceProduto.Excluir(id);

            return CustomResponse(id);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<ProdutoViewModelResponse>> ObterTodos()
        {            
            return _mapper.Map<IEnumerable<ProdutoViewModelResponse>>(await _appServiceProduto.ObterTodos());
        }
    }
}
