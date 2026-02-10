using Microsoft.Extensions.DependencyInjection;

using CheckMyStar.Bll.Criteres;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Criteres;

namespace CheckMyStar.Apis.Services.Extensions
{
    public static class CriteresServiceCollectionExtensions
    {
        public static IServiceCollection AddCriteresServices(this IServiceCollection services)
        {
            services.AddScoped<ICriteresService, CriteresService>();

            services.AddScoped<ICriteresRepository, CriteresRepository>();

            return services;
        }
    }
}
