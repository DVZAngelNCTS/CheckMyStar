using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface ICountryBus
    {
        Task<CountriesResponse> GetCountries(CancellationToken ct);
    }
}
