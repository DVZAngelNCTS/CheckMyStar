using Microsoft.Extensions.DependencyInjection;

using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;

namespace CheckMyStar.Bll.Services.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services
                .AddScoped<IGeolocationService, GeolocationService>();

            services
                .AddScoped<IGeolocationBusForService>(x => (GeolocationService)x.GetRequiredService<IGeolocationService>());

            return services;
        }
    }
}
