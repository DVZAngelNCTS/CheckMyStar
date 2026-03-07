using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService
{
    public interface IAccommodationBusForService
    {
        Task<AccommodationResponse> GetNextIdentifier(CancellationToken ct);
        Task<AccommodationsResponse> GetAccommodations(CancellationToken ct);
        Task<BaseResponse> AddAccommodation(AccommodationSaveRequest request, CancellationToken ct);
        Task<BaseResponse> UpdateAccommodation(AccommodationSaveRequest request, CancellationToken ct);
        Task<BaseResponse> DeleteAccommodation(AccommodationDeleteRequest request, CancellationToken ct);
        Task<BaseResponse> EnabledAccommodation(AccommodationSaveRequest request, CancellationToken ct);
    }
}
