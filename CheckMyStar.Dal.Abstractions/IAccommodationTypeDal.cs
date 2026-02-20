using CheckMyStar.Dal.Results;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IAccommodationTypeDal
    {
        Task<AccommodationTypeResult> GetAccommodationType(int accommodationTypeIdentifier, CancellationToken ct);
    }
}
