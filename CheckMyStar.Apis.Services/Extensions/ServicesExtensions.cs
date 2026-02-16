using Microsoft.Extensions.DependencyInjection;

using CheckMyStar.Apis.Services.Abstractions;

namespace CheckMyStar.Apis.Services.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddScoped<IAuthenticateService, AuthenticateService>()
                .AddScoped<IUserContextService, UserContextService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IRoleService, RoleService>()
                .AddScoped<ICountryService, CountryService>()
                .AddScoped<IAddressService, AddressService>()
                .AddScoped<IDashboardService, DashboardService>()
                .AddScoped<IActivityService, ActivityService>()
                .AddScoped<ICriteriaService, CriteriaService>()
                .AddScoped<ISocietyService, SocietyService>();

            return services;
        }
    }
}
