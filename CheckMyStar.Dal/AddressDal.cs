using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data.Abstractions;
using CheckMyStar.Dal.Results;

namespace CheckMyStar.Dal
{
    public class AddressDal(ICheckMyStarDbContext dbContext) : IAddressDal
    {
        public async Task<AddressResult> GetAddress(int addressIdentifier, CancellationToken ct)
        {
            AddressResult addressResult = new AddressResult();

            try
            {
                var address = await (from a in dbContext.Addresses
                                     where a.Identifier == addressIdentifier
                                     select a).FirstOrDefaultAsync(ct);

                addressResult.IsSuccess = true;
                addressResult.Address = address;
            }
            catch (Exception ex)
            {
                addressResult.IsSuccess = false;
                addressResult.Message = ex.Message;
            }

            return addressResult;
        }
    }
}
