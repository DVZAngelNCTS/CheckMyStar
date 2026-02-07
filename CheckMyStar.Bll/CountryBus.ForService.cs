using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll
{
    public partial class CountryBus : ICountryBusForService
    {
        public Task<CountriesResponse> GetAllCountries(CancellationToken ct)
        {
            return this.GetCountries(ct);
        }
    }
}
