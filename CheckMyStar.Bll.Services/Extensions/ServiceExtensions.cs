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
                .AddScoped<IGeolocationService, GeolocationService>()
                .AddScoped<ISendMailService, SendMailService>();

            services
                .AddScoped<IGeolocationForService>(x => (GeolocationService)x.GetRequiredService<IGeolocationService>())
                .AddScoped<ISendMailForService>(x => (SendMailService)x.GetRequiredService<ISendMailService>());

            return services;
        }
    }
}
