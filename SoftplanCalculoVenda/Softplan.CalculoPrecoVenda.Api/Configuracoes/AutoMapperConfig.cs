using AutoMapper;
using Softplan.CalculoPrecoVenda.Api.ViewModels;
using Softplan.CalculoPrecoVenda.Domain.Entidades;

namespace Softplan.CalculoPrecoVenda.Api.Configuracoes
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<CategoriaResponseViewModel, Categoria>().ReverseMap();
            CreateMap<CategoryRequestViewModel, Categoria>().ReverseMap();
        }
    }
}
