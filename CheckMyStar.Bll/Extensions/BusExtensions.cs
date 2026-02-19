using Microsoft.Extensions.DependencyInjection;

using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;

namespace CheckMyStar.Bll.Extensions
{
    public static class BusExtensions
    {
        public static IServiceCollection AddBus(this IServiceCollection services)
        {
            services
                .AddScoped<IUserBus, UserBus>()
                .AddScoped<IRoleBus, RoleBus>()
                .AddScoped<ICountryBus, CountryBus>()
                .AddScoped<IAddressBus, AddressBus>()
                .AddScoped<IDashboardBus, DashboardBus>()
                .AddScoped<IActivityBus, ActivityBus>()
                .AddScoped<ICriteriaBus, CriteriaBus>()
                .AddScoped<ISocietyBus, SocietyBus>()
                .AddScoped<IAccommodationBus, AccommodationBus>()
                .AddScoped<IFolderBus, FolderBus>()
                .AddScoped<IAssessmentBus, AssessmentBus>();

            services
                .AddScoped<IUserBusForService>(x => (UserBus)x.GetRequiredService<IUserBus>())
                .AddScoped<IRoleBusForService>(x => (RoleBus)x.GetRequiredService<IRoleBus>())
                .AddScoped<ICountryBusForService>(x => (CountryBus)x.GetRequiredService<ICountryBus>())
                .AddScoped<IAddressBusForService>(x => (AddressBus)x.GetRequiredService<IAddressBus>())
                .AddScoped<IDashboardBusForService>(x => (DashboardBus)x.GetRequiredService<IDashboardBus>())
                .AddScoped<IActivityBusForService>(x => (ActivityBus)x.GetRequiredService<IActivityBus>())
                .AddScoped<ICriteriaBusForService>(x => (CriteriaBus)x.GetRequiredService<ICriteriaBus>())
                .AddScoped<ISocietyBusForService>(x => (SocietyBus)x.GetRequiredService<ISocietyBus>())
                .AddScoped<IAccommodationBusForService>(x => (AccommodationBus)x.GetRequiredService<IAccommodationBus>())
                .AddScoped<IFolderBusForService>(x => (FolderBus)x.GetRequiredService<IFolderBus>())
                .AddScoped<IAssessmentBusForService>(x => (AssessmentBus)x.GetRequiredService<IAssessmentBus>());

            return services;
        }
    }
}
