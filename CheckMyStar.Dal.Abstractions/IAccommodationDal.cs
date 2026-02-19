using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IAccommodationDal
    {
        Task<AccommodationResult> GetNextIdentifier(CancellationToken ct);
        Task<AccommodationResult> GetAccommodation(int accommodationIdentifier, CancellationToken ct);
        Task<AccommodationsResult> GetAccommodations(CancellationToken ct);
        Task<BaseResult> AddAccommodation(Accommodation accommodation, CancellationToken ct);
        Task<BaseResult> UpdateAccommodation(Accommodation accommodation, CancellationToken ct);
        Task<BaseResult> DeleteAccommodation(Accommodation accommodation, CancellationToken ct);
    }
}
