using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal
{
    public class AccommodationTypeDal(ICheckMyStarDbContext dbContext) : IAccommodationTypeDal
    {
        public async Task<AccommodationTypeResult> GetAccommodationType(int accommodationTypeIdentifier, CancellationToken ct)
        {
            AccommodationTypeResult accommodationTypeResult = new AccommodationTypeResult();

            try
            {
                var accommodationType = await (from at in dbContext.AccommodationTypes.AsNoTracking()
                                              where at.Identifier == accommodationTypeIdentifier
                                              select at).FirstOrDefaultAsync(ct);

                accommodationTypeResult.IsSuccess = true;
                accommodationTypeResult.AccommodationType = accommodationType;
            }
            catch (Exception ex)
            {
                accommodationTypeResult.IsSuccess = false;
                accommodationTypeResult.Message = ex.Message;
            }

            return accommodationTypeResult;
        }
    }
}
