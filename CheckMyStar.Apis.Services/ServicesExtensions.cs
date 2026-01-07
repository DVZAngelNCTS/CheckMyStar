using Microsoft.Extensions.DependencyInjection;

using CheckMyStar.Apis.Services.Abstractions;

namespace CheckMyStar.Apis.Services
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddScoped<IAuthenticateService, AuthenticateService>()
                .AddScoped<IUserContextService, UserContextService>();

            return services;
        }
    }
}
