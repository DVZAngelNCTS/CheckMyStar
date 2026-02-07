using Microsoft.Extensions.DependencyInjection;

using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;

namespace CheckMyStar.Bll
{
    public static class BusExtensions
    {
        public static IServiceCollection AddBus(this IServiceCollection services)
        {
            services
                .AddScoped<IUserBus, UserBus>()
                .AddScoped<IRoleBus, RoleBus>()
                .AddScoped<ICountryBus, CountryBus>()
                .AddScoped<IAddressBus, AddressBus>();

            services
                .AddScoped<IUserBusForService>(x => (UserBus)x.GetRequiredService<IUserBus>())
                .AddScoped<IRoleBusForService>(x => (RoleBus)x.GetRequiredService<IRoleBus>())
                .AddScoped<ICountryBusForService>(x => (CountryBus)x.GetRequiredService<ICountryBus>())
                .AddScoped<IAddressBusForService>(x => (AddressBus)x.GetRequiredService<IAddressBus>());

            return services;
        }
    }
}
