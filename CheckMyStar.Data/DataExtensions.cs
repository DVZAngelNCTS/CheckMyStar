using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Data
{
    public static class DataExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<CheckMyStarDbContext>(
                    (serviceProvider, options) =>
                    {
                        options.UseSqlServer(configuration.GetConnectionString("CheckMyStarConnection"));
                    })
                .AddScoped<ICheckMyStarDbContext>(p => p.GetRequiredService<CheckMyStarDbContext>());

            return services;
        }
    }
}
