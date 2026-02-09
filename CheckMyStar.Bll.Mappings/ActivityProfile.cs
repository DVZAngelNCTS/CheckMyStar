using AutoMapper;

using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Models;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Bll.Mappings
{
    public class ActivityProfile : Profile
    {
        public ActivityProfile()
        {
            CreateMap<Activity, ActivityModel>()
                .ForMember(dest => dest.Identifier, opts => opts.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Date))
                .ForMember(dest => dest.IsSuccess, opts => opts.MapFrom(src => src.IsSuccess));

            CreateMap<UserActivity, ActivityModel>()
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Date))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(dest => dest.Identifier, opts => opts.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.IsSuccess, opts => opts.MapFrom(src => src.IsSuccess))
                .ForMember(dest => dest.User, opts => opts.MapFrom(src => src.User));

            CreateMap<ActivityResult, ActivityResponse>()
                .ForMember(dest => dest.Activity, opts => opts.MapFrom(src => src.Activity ?? null));

            CreateMap<ActivitiesResult, ActivitiesResponse>()
                .ForMember(dest => dest.Activities, opts => opts.MapFrom(src => src.Activities ?? null));
        }
    }
}
