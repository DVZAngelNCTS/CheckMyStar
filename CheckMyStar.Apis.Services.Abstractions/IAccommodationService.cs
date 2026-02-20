using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services.Abstractions
{
    public interface IAccommodationService
    {
        Task<AccommodationResponse> GetNextIdentifier(CancellationToken ct);
        Task<AccommodationsResponse> GetAccommodations(CancellationToken ct);
        Task<BaseResponse> CreateAccommodation(AccommodationSaveRequest request, CancellationToken ct);
        Task<BaseResponse> DeleteAccommodation(int accommodationIdentifier, CancellationToken ct);
    }
}
