using AutoMapper;

using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Bll.Mappings
{
    public class FolderProfile : Profile
    {
        public FolderProfile()
        {
            CreateMap<Folder, FolderModel>()
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate))
                .ForMember(dest => dest.AccommodationType, opt => opt.Ignore())
                .ForMember(dest => dest.Accommodation, opt => opt.Ignore())
                .ForMember(dest => dest.OwnerUser, opt => opt.Ignore())
                .ForMember(dest => dest.InspectorUser, opt => opt.Ignore())
                .ForMember(dest => dest.FolderStatus, opt => opt.Ignore())
                .ForMember(dest => dest.Quote, opt => opt.Ignore())
                .ForMember(dest => dest.Invoice, opt => opt.Ignore())
                .ForMember(dest => dest.Appointment, opt => opt.Ignore());

            CreateMap<FolderModel, Folder>()
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.AccommodationTypeIdentifier, opt => opt.MapFrom(src => src.AccommodationType.Identifier))
                .ForMember(dest => dest.AccommodationIdentifier, opt => opt.MapFrom(src => src.Accommodation.Identifier))
                .ForMember(dest => dest.OwnerUserIdentifier, opt => opt.MapFrom(src => src.OwnerUser.Identifier))
                .ForMember(dest => dest.InspectorUserIdentifier, opt => opt.MapFrom(src => src.InspectorUser.Identifier))
                .ForMember(dest => dest.FolderStatusIdentifier, opt => opt.MapFrom(src => src.FolderStatus.Identifier))
                .ForMember(dest => dest.QuoteIdentifier, opt => opt.MapFrom(src => src.Quote != null ? src.Quote.Identifier : (int?)null))
                .ForMember(dest => dest.InvoiceIdentifier, opt => opt.MapFrom(src => src.Invoice != null ? src.Invoice.Identifier : (int?)null))
                .ForMember(dest => dest.AppointmentIdentifier, opt => opt.MapFrom(src => src.Appointment != null ? src.Appointment.Identifier : (int?)null))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate));

            CreateMap<FolderStatus, FolderStatusModel>()
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier));

            CreateMap<FolderStatusModel, FolderStatus>()
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier));

            CreateMap<Quote, QuoteModel>()
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.ValidUntilDate, opt => opt.MapFrom(src => src.ValidUntilDate))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<QuoteModel, Quote>()
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.ValidUntilDate, opt => opt.MapFrom(src => src.ValidUntilDate))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<Invoice, InvoiceModel>()
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src => src.InvoiceNumber))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
                .ForMember(dest => dest.IsPaid, opt => opt.MapFrom(src => src.IsPaid));

            CreateMap<InvoiceModel, Invoice>()
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src => src.InvoiceNumber))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
                .ForMember(dest => dest.IsPaid, opt => opt.MapFrom(src => src.IsPaid));

            CreateMap<Appointment, AppointmentModel>()
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.AppointmentDate))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate));

            CreateMap<AppointmentModel, Appointment>()
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.AppointmentDate))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate));

            CreateMap<FolderResult, FolderResponse>()
                .ForMember(dest => dest.Folder, opt => opt.MapFrom(src => src.Folder ?? null))
                .ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.IsSuccess))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message));
        }
    }
}
