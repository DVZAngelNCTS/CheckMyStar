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
                .AddScoped<IUserBus, UserBus>();

            services
                .AddScoped<IUserBusForService>(x => (UserBus)x.GetRequiredService<IUserBus>());

            return services;
        }
    }
}
