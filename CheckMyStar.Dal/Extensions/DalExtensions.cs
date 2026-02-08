using Microsoft.Extensions.DependencyInjection;

using CheckMyStar.Dal.Abstractions;

namespace CheckMyStar.Dal.Extensions
{
    public static class DalExtensions
    {
        public static IServiceCollection AddDal(this IServiceCollection services)
        {
            services
                .AddScoped<IUserDal, UserDal>()
                .AddScoped<ICivilityDal, CivilityDal>()
                .AddScoped<IRoleDal, RoleDal>()
                .AddScoped<ICivilityDal, CivilityDal>()
                .AddScoped<ICountryDal, CountryDal>()
                .AddScoped<IAddressDal, AddressDal>()
                .AddScoped<IDashboardDal, DashboardDal>()
                .AddScoped<IActivityDal, ActivityDal>();

            return services;
        }
    }
}
