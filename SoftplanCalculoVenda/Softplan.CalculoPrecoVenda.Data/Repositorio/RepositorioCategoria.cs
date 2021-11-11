using Softplan.CalculoPrecoVenda.Data.Contexto;
using Softplan.CalculoPrecoVenda.Domain.Contratos.Repositorio;
using Softplan.CalculoPrecoVenda.Domain.Entidades;

namespace Softplan.CalculoPrecoVenda.Data.Repositorio
{
    public class RepositorioCategoria : RepositorioBase<Categoria>, IRepositorioCategoria
    {
        public RepositorioCategoria(SoftplanDbContext context) : base(context) { }
    }
}
