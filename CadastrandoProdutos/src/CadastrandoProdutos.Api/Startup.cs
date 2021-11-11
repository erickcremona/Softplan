using CadastroProdutos.Infra.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CadastrandoProdutos.Api.Settings;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace CadastrandoProdutos.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<CadastroProdutosContext>(option => 
                option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentityConfig(Configuration);
            services.AddApiConfig();
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerConfig();
            services.ResolveDependency();
            
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseApiConfig(env);

            app.UseSwaggerConfig(provider);
        }
    }
}
