using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data.Abstractions;
using CheckMyStar.Data;

namespace CheckMyStar.Dal
{
    public class CivilityDal(ICheckMyStarDbContext dbContext) : ICivilityDal
    {
        public async Task<List<Civility>> GetCivilities()
        {
            var civilities = await (from c in dbContext.Civilities
                                    orderby c.Name
                                    select c).ToListAsync();

            return civilities;
        }
    }
}
