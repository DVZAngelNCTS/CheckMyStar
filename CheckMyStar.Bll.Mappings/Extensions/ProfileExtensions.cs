using Microsoft.Extensions.DependencyInjection;

namespace CheckMyStar.Bll.Mappings.Extensions
{
    public static class ProfileExtensions
    {
        public static IServiceCollection AddProfile(this IServiceCollection services)
        {
            services
                .AddAutoMapper(typeof(UserProfile))
                .AddAutoMapper(typeof(CivilityProfile))
                .AddAutoMapper(typeof(AddressProfile))
                .AddAutoMapper(typeof(CountryProfile))
                .AddAutoMapper(typeof(RoleProfile))
                .AddAutoMapper(typeof(BaseProfile))
                .AddAutoMapper(typeof(DashboardProfile))
                .AddAutoMapper(typeof(ActivityProfile));

            return services;
        }
    }
}
