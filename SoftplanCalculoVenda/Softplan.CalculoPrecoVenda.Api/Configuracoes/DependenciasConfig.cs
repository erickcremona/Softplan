using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Softplan.CalculoPrecoVenda.Data.Contexto;
using Softplan.CalculoPrecoVenda.Data.Repositorio;
using Softplan.CalculoPrecoVenda.Domain.Contratos;
using Softplan.CalculoPrecoVenda.Domain.Contratos.Repositorio;
using Softplan.CalculoPrecoVenda.Domain.Contratos.Servico;
using Softplan.CalculoPrecoVenda.Domain.Notificacoes;
using Softplan.CalculoPrecoVenda.Domain.Servicos;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Softplan.CalculoPrecoVenda.Api.Configuracoes
{
    public static class DependenciasConfig
    {
        public static void ResolverDependencias(this IServiceCollection services)
        {
            services.AddScoped<SoftplanDbContext>();
            services.AddScoped<IRepositorioCategoria, RepositorioCategoria>();
            services.AddScoped<IServicoCategoria, ServicoCategoria>();
            services.AddScoped<INotificador, Notificador>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        }
    }
}
