using CadastroProdutos.Domain.Entities;
using CadastroProdutos.Domain.Interfaces;
using CadastroProdutos.Domain.Interfaces.Repository;
using CadastroProdutos.Domain.Interfaces.Service;
using CadastroProdutos.Domain.Validations;
using System;
using System.Threading.Tasks;

namespace CadastroProdutos.Domain.Services
{
    public class DomainServiceProduto: DomainServiceBase<Produto>, IDomainServiceProduto
    {
        private readonly IRepositoryProduto _repositoryProduto;
        private readonly IHttpService _httpService;
        public DomainServiceProduto(IRepositoryProduto repositoryProduto, INotificador notificador, IHttpService httpService) : base(repositoryProduto, notificador)
        {
            _repositoryProduto = repositoryProduto;
            _httpService = httpService;
        }

        public async Task AdicionarProduto(Produto produto)
        {
            if(produto.VerificarCategoriaSoftplan(produto.Descricao))
            produto.Categoria = "Softplan";

            produto.PrecoVenda = await _httpService.ObterPrecoVenda(produto.PrecoCusto, produto.Categoria);

            if (Validar(produto))
                Adicionar(produto);    
        }

        public async Task AlterarProduto(Produto produto)
        {
            if(produto.VerificarCategoriaSoftplan(produto.Descricao))
            produto.Categoria = "Softplan";

            produto.PrecoVenda = await _httpService.ObterPrecoVenda(produto.PrecoCusto, produto.Categoria);

            if (Validar(produto))
                Alterar(produto); 
        }

        public async Task ExcluirProduto(Guid id)
        {
            var produto = await _repositoryProduto.ObterPorIdAsync(id);

            if (produto == null)
                Notificar("Id do Produto não localizado");
            
            if(!_notificador.TemNotificacao())
                Excluir(produto);
        }

        public bool Validar(Produto produto)
            => ExecutarValicacao(new ValidacaoProduto(), produto);
    }
}
