using CheckMyStar.Dal.Results;

namespace CheckMyStar.Dal.Abstractions
{
    public interface ICountryDal
    {
        Task<CountriesResult> GetCountries(CancellationToken ct);
        Task<CountryResult> GetCountry(int identifier, CancellationToken ct);
    }
}
