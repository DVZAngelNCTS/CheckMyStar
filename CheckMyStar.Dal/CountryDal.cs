using Microsoft.EntityFrameworkCore;

using CheckMyStar.Data.Abstractions;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;

namespace CheckMyStar.Dal
{
    public class CountryDal(ICheckMyStarDbContext dbContext) : ICountryDal
    {
        public async Task<CountriesResult> GetCountries(CancellationToken ct)
        {
            CountriesResult countriesResult = new CountriesResult();

            try
            {
                var countries = await (from c in dbContext.Countries
                                       orderby c.Name
                                       select c).ToListAsync(ct);

                countriesResult.IsSuccess = true;
                countriesResult.Countries = countries;

            }
            catch (Exception ex)
            {
                countriesResult.IsSuccess = false;
                countriesResult.Message = ex.Message;
            }

            return countriesResult;
        }
    }
}
