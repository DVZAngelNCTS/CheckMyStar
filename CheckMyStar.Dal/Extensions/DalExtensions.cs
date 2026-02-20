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
                .AddScoped<IActivityDal, ActivityDal>()
                .AddScoped<ICriteresDal, CriteriaDal>()
                .AddScoped<ISocietyDal, SocietyDal>()
                .AddScoped<IAccommodationDal, AccommodationDal>()
                .AddScoped<IAccommodationTypeDal, AccommodationTypeDal>()
                .AddScoped<IFolderDal, FolderDal>()
                .AddScoped<IFolderStatusDal, FolderStatusDal>()
                .AddScoped<IQuoteDal, QuoteDal>()
                .AddScoped<IInvoiceDal, InvoiceDal>()
                .AddScoped<IAppointmentDal, AppointmentDal>()
                .AddScoped<IAssessmentDal, AssessmentDal>();

            return services;
        }
    }
}
