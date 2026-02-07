using AutoMapper;

using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;


namespace CheckMyStar.Bll
{
    public partial class CountryBus(ICountryDal countryDal, IMapper mapper) : ICountryBus
    {
        public async Task<CountriesResponse> GetCountries(CancellationToken ct)
        {
            var countries = await countryDal.GetCountries(ct);

            return mapper.Map<CountriesResponse>(countries);
        }
    }
}
