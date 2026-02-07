using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services.Abstractions
{
    public interface ICountryService
    {
        Task<CountriesResponse> GetCountries(CancellationToken ct);
    }
}
