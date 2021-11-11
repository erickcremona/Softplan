using CadastroProdutos.Application.Interfaces;
using CadastroProdutos.Application.Services;
using CadastroProdutos.Domain.Interfaces;
using CadastroProdutos.Domain.Interfaces.Repository;
using CadastroProdutos.Domain.Interfaces.Service;
using CadastroProdutos.Domain.Notifications;
using CadastroProdutos.Domain.Services;
using CadastroProdutos.Infra.Data.Context;
using CadastroProdutos.Infra.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CadastrandoProdutos.Api.Settings
{
    public static class DependenciesConfig
    {
        public static void ResolveDependency(this IServiceCollection services)
        {
            services.AddScoped<CadastroProdutosContext>();
            services.AddScoped<IRepositoryProduto, RepositoryProduto>();
            services.AddScoped<IDomainServiceProduto, DomainServiceProduto>();
            services.AddScoped<IAppServiceProduto, AppServiceProduto>();
            services.AddScoped<INotificador, Notificador>();
            services.AddHttpClient<IHttpService, HttpService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        }
    }
}
