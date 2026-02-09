using AutoMapper;

using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Models;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;
using CheckMyStar.Enumerations;

namespace CheckMyStar.Bll.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Identifier, opts => opts.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Password, opts => opts.MapFrom(src => src.Password))
                .ForMember(dest => dest.Phone, opts => opts.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Society, opts => opts.MapFrom(src => src.Society))
                .ForMember(dest => dest.Civility, opts => opts.MapFrom(src => src.CivilityIdentifier.ToEnum<EnumCivility>()))
                .ForMember(dest => dest.Role, opts => opts.MapFrom(src => src.RoleIdentifier.ToEnum<EnumRole>()))
                .ForMember(dest => dest.IsActive, opts => opts.MapFrom(src => src.IsActive));

            CreateMap<UserModel, User>()
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Identifier, opts => opts.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Password, opts => opts.MapFrom(src => src.Password))
                .ForMember(dest => dest.Phone, opts => opts.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Society, opts => opts.MapFrom(src => src.Society))
                .ForMember(dest => dest.CivilityIdentifier, opts => opts.MapFrom(src => src.Civility.ToInt()))
                .ForMember(dest => dest.RoleIdentifier, opts => opts.MapFrom(src => src.Role.ToInt()))
                .ForMember(dest => dest.IsActive, opts => opts.MapFrom(src => src.IsActive));

            CreateMap<UserResult, UserResponse>()
                .ForMember(dest => dest.User, opts => opts.MapFrom(src => src.User ?? null));

            CreateMap<UsersResult, UsersResponse>()
                .ForMember(dest => dest.Users, opts => opts.MapFrom(src => src.Users ?? null));

            CreateMap<UserEvolution, UserEvolutionModel>()
                .ForMember(dest => dest.Year, opts => opts.MapFrom(src => src.Year))
                .ForMember(dest => dest.Month, opts => opts.MapFrom(src => src.Month))
                .ForMember(dest => dest.IsActive, opts => opts.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.IsDisabled, opts => opts.MapFrom(src => src.IsDisabled))
                .ForMember(dest => dest.Total, opts => opts.MapFrom(src => src.Total));

            CreateMap<UserEvolutionResult, UserEvolutionResponse>()
                .ForMember(dest => dest.Evolutions, opts => opts.MapFrom(src => src.Evolutions ?? null));
        }
    }
}
