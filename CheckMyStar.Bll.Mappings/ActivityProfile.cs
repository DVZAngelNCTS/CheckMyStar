using AutoMapper;

using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
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
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Date));

            CreateMap<ActivityResult, ActivityResponse>()
                .ForMember(dest => dest.Activity, opts => opts.MapFrom(src => src.Activity ?? null));

            CreateMap<ActivitiesResult, ActivitiesResponse>()
                .ForMember(dest => dest.Activities, opts => opts.MapFrom(src => src.Activities ?? null));
        }
    }
}
