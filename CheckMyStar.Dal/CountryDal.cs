using Microsoft.EntityFrameworkCore;

using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;
using CheckMyStar.Dal.Abstractions;

namespace CheckMyStar.Dal
{
    public class CountryDal(ICheckMyStarDbContext dbContext) : ICountryDal
    {
        public async Task<List<Country>> GetCountries(CancellationToken ct)
        {
            var countries = await (from c in dbContext.Countries
                                   orderby c.Name
                                   select c).ToListAsync(ct);

            return countries;
        }
    }
}
