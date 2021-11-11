using CadastroProdutos.Application.Interfaces;
using CadastroProdutos.Domain.Entities;
using CadastroProdutos.Domain.Interfaces;
using CadastroProdutos.Domain.Interfaces.Repository;
using CadastroProdutos.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroProdutos.Application.Services
{
    public class AppServiceProduto : IAppServiceProduto
    {
        private readonly IDomainServiceProduto _domainService;
        private readonly IRepositoryProduto _repositoryProduto;
        private readonly INotificador _notificador;
        private readonly IHttpService _httpService;

        public AppServiceProduto(IDomainServiceProduto domainService, INotificador notificador, IRepositoryProduto repositoryProduto, IHttpService httpService)
        {
            _domainService = domainService;
            _repositoryProduto = repositoryProduto;
            _notificador = notificador;
            _httpService = httpService;
        }

        public async Task Adicionar(Produto produto)
        {           
            await _domainService.AdicionarProduto(produto);

            if (!_notificador.TemNotificacao())
                await _domainService.SaveChanges();
        }

        public async Task Alterar(Produto produto)
        {
            await _domainService.AlterarProduto(produto);

            if (!_notificador.TemNotificacao())
                await _domainService.SaveChanges();
        }

        public async Task Excluir(Guid id)
        {
             await _domainService.ExcluirProduto(id);

            if (!_notificador.TemNotificacao())
                await _domainService.SaveChanges();
        }

        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await _repositoryProduto.ObterTodosOrdenadosPorDescricao();
        }

       
    }
}
