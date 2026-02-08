using AutoMapper;

using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Models;
using CheckMyStar.Dal.Results;

namespace CheckMyStar.Bll.Mappings
{
    public class DashboardProfile : Profile
    {
        public DashboardProfile()
        {
            CreateMap<Dashboard, DashboardModel>()
                .ForMember(dest => dest.NumberRoles, opts => opts.MapFrom(src => src.NumberRoles))
                .ForMember(dest => dest.NumberUserActives, opts => opts.MapFrom(src => src.NumberUserActives))
                .ForMember(dest => dest.NumberUserMonthlyTrend, opts => opts.MapFrom(src => src.NumberUserMonthlyTrend))
                .ForMember(dest => dest.NumberUsers, opts => opts.MapFrom(src => src.NumberUsers))
                .ForMember(dest => dest.NumberUserWeeklyTrend, opts => opts.MapFrom(src => src.NumberUserWeeklyTrend))
                .ForMember(dest => dest.NumberUserYearlyTrend, opts => opts.MapFrom(src => src.NumberUserYearlyTrend))
                .ForMember(dest => dest.PercentageUserActive, opts => opts.MapFrom(src => src.PercentageUserActive))
                .ForMember(dest => dest.PercentageUserWeeklyTrend, opts => opts.MapFrom(src => src.PercentageUserWeeklyTrend))
                .ForMember(dest => dest.PercentageUserMonthlyTrend, opts => opts.MapFrom(src => src.PercentageUserMonthlyTrend))
                .ForMember(dest => dest.PercentageUserYearlyTrend, opts => opts.MapFrom(src => src.PercentageUserYearlyTrend));


            CreateMap<DashboardResult, DashboardResponse>()
                .ForMember(dest => dest.Dashboard, opts => opts.MapFrom(src => src.Dashboard ?? null));
        }
    }
}
