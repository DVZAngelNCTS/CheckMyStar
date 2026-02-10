using Microsoft.Extensions.DependencyInjection;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Criteres;

namespace CheckMyStar.Dal.Extensions
{
    public static class CriteresDalExtensions
    {
        public static IServiceCollection AddCriteresDal(this IServiceCollection services)
        {
            // Register the DAL implementation for criteres
            services.AddScoped<ICriteresRepository, CriteresRepository>();
            return services;
        }
    }
}
