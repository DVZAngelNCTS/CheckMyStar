using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data.Abstractions;
using CheckMyStar.Data;

namespace CheckMyStar.Dal
{
    public class AddressDal(ICheckMyStarDbContext dbContext) : IAddressDal
    {
        public async Task<Address?> GetAddress(int addressIdentifier, CancellationToken ct)
        {
            var address = await (from a in dbContext.Addresses
                                 where a.Identifier == addressIdentifier
                                 select a).FirstOrDefaultAsync(ct);

            return address;
        }
    }
}
