using AutoMapper;

using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Bll.Mappings
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentModel>();

            CreateMap<AppointmentModel, Appointment>();

            CreateMap<AppointmentResult, AppointmentResponse>()
                .ForMember(dest => dest.Appointment, opts => opts.MapFrom(src => src.Appointment));
        }
    }
}
