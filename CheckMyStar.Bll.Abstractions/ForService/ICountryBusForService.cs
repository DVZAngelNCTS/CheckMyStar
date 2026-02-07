using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService
{
    public interface ICountryBusForService
    {
        Task<CountriesResponse> GetAllCountries(CancellationToken ct);
    }
}
