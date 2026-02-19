using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IAccommodationBus
    {
        Task<AccommodationResponse> GetIdentifier(CancellationToken ct);
        Task<AccommodationsResponse> GetAccommodations(CancellationToken ct);
        Task<BaseResponse> AddAccommodation(AccommodationModel accommodationModel, int currentUser, CancellationToken ct);
        Task<BaseResponse> DeleteAccommodation(int accommodationIdentifier, int currentUser, CancellationToken ct);
    }
}
