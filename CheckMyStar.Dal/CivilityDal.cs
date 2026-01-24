using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data.Abstractions;
using CheckMyStar.Data;
using CheckMyStar.Dal.Results;

namespace CheckMyStar.Dal
{
    public class CivilityDal(ICheckMyStarDbContext dbContext) : ICivilityDal
    {
        public async Task<CivilityResult> GetCivilities(CancellationToken ct)
        {
            CivilityResult civilityResult = new CivilityResult();

            try
            {
                var civilities = await (from c in dbContext.Civilities
                                        orderby c.Name
                                        select c).AsNoTracking().ToListAsync(ct);

                civilityResult.Civilities = civilities;
                civilityResult.IsSuccess = true;

            }
            catch (Exception ex)
            {
                civilityResult.IsSuccess = false;
                civilityResult.Message = ex.Message;
            }


            return civilityResult;
        }
    }
}
