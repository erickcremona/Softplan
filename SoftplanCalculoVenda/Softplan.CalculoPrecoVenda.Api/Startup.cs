using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Softplan.CalculoPrecoVenda.Api.Configuracoes;
using Softplan.CalculoPrecoVenda.Data.Contexto;

namespace Softplan.CalculoPrecoVenda.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {           
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Carregar banco de dados na memória
            // services.AddDbContext<SoftplanDbContext>(option => option.UseInMemoryDatabase("SoftplanDb"));

            // Configure a ConnectionString no appsettings.json para utilizar banco de dados SQLServer
            services.AddDbContext<SoftplanDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentityConfig(Configuration);
            services.AddApiConfig();
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerConfig();
            services.ResolverDependencias();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        { 
            app.UseApiConfig(env);

            app.UseSwaggerConfig(provider);
        }
    }
}
