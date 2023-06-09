using Catalog.Core.Repositories;
using Catalog.Infraestructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infraestructure.Data.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfrestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICatalogContext, CatalogContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBrandRepository, ProductRepository>();
            services.AddScoped<ITypesRepository, ProductRepository>();
            return services;
        }
    }
}
