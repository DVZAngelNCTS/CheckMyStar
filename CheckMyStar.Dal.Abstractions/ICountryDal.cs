using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface ICountryDal
    {
        Task<CountriesResult> GetCountries(CancellationToken ct);
    }
}
