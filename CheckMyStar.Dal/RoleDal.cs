using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal
{
    public class RoleDal(ICheckMyStarDbContext dbContext) : IRoleDal
    {
        public async Task<List<Role>> GetRoles(string? name, CancellationToken ct)
        {
            var roles = await (from r in dbContext.Roles
                               where
                                string.IsNullOrEmpty(name) || r.Name.Contains(name)
                               orderby r.Name
                               select r).ToListAsync(ct);

            return roles;
        }
    }
}
