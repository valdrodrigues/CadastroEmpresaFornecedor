using Microsoft.Extensions.DependencyInjection;
using System;
using Teste.Dev.Domain.IApplication;
using Teste.Dev.Domain.IRepository;

namespace Teste.Dev.Infra.CrossCutting.IoC
{
    public static class InjectorConfig
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient(typeof(Lazy<>), typeof(Lazier<>));

            //Aplication
            services.AddScoped<IEmpresaApplication>();
            services.AddScoped<IFornecedorApplication>();

            //Infra
            services.AddScoped<IEmpresaRepository>();
            services.AddScoped<IFornecedorRepository>();
        }
    }
}
