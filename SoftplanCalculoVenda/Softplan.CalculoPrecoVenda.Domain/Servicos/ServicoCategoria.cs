using Softplan.CalculoPrecoVenda.Domain.Contratos;
using Softplan.CalculoPrecoVenda.Domain.Contratos.Repositorio;
using Softplan.CalculoPrecoVenda.Domain.Contratos.Servico;
using Softplan.CalculoPrecoVenda.Domain.Entidades;
using Softplan.CalculoPrecoVenda.Domain.Validacoes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Softplan.CalculoPrecoVenda.Domain.Servicos
{
    public class ServicoCategoria : ServicoBase<Categoria>, IServicoCategoria
    {
        private readonly INotificador _notificador;

        public ServicoCategoria(IRepositorioCategoria repositorioCategoria,
                                INotificador notificador) :
                                base(repositorioCategoria,
                                     notificador)
        {
            _notificador = notificador;
        }


        public async Task<int> AdicionarCategoria(Categoria categoria)
        {
            Validar(categoria);

            if (ObterPorExprecaoAsync(c => c.Nome == categoria.Nome).Result.Any())
                Notificar("Categoria já encontra-se cadastrada");

            if (_notificador.TemNotificacao()) return 0;

            categoria.Lucro = categoria.VerificarTipoCategoria(categoria.Nome);

            Adicionar(categoria);
            return await SaveChange();
        }

        public async Task<int> AlterarCategoria(Categoria categoria)
        {
            Validar(categoria);

            var categoriaAlterar = await ObterPorIdAsync(categoria.Id);

            if (categoriaAlterar == null) Notificar("Categoria não localizada");

            if (_notificador.TemNotificacao()) return 0;

            categoriaAlterar.Nome = categoria.Nome;
            categoriaAlterar.Lucro = categoria.VerificarTipoCategoria(categoria.Nome);

            Alterar(categoriaAlterar);
            return await SaveChange();
        }

        public double CalcularPrecoVenda(Categoria categoria, double precoCusto)
        {
            return precoCusto + (precoCusto * categoria.Lucro) / 100;
        }

        public async Task<Categoria> ExcluirCategoria(Guid id)
        {
            var entity = await ObterPorIdAsync(id);

            if (entity == null)
                Notificar("Categoria não localizada");

            if (!_notificador.TemNotificacao())
            {
                Excluir(entity);
                await SaveChange();
            }

            return entity;
        }

        public async Task<Categoria> ObterCategoriaPorNome(string nomeCategoria)
        {
            var categoria = await ObterPorExprecaoAsync(p => p.Nome == nomeCategoria);

            return categoria.FirstOrDefault();
        }


        public bool Validar(Categoria categoria)
            => ExecutarValidacao(new CategoriaValidacao(), categoria);
    }
}
