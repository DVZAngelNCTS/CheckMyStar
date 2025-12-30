using Microsoft.Extensions.DependencyInjection;

namespace CheckMyStar.Bll.Mappings
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
                .AddAutoMapper(typeof(RoleProfile));

            return services;
        }
    }
}
