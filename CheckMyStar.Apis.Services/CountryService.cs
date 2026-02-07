using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services
{
    public class CountryService(ICountryBusForService countryBusForService) : ICountryService
    {
        public async Task<CountriesResponse> GetCountries(CancellationToken ct)
        {
            var countries = await countryBusForService.GetAllCountries(ct);

            return countries;
        }
    }
}
