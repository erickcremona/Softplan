
using AutoMapper;
using CadastrandoProdutos.Api.ViewModels;
using CadastroProdutos.Domain.Entities;

namespace CadastrandoProdutos.Api.Settings
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ProdutoViewModelRequest, Produto>().ReverseMap();
            CreateMap<ProdutoViewModelResponse, Produto>().ReverseMap();
        }
    }
}
