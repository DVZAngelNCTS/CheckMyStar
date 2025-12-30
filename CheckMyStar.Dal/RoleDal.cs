using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal
{
    public class RoleDal(ICheckMyStarDbContext dbContext) : IRoleDal
    {
        public async Task<List<Role>> GetRoles()
        {
            var roles = await (from r in dbContext.Roles
                               orderby r.Name
                               select r).ToListAsync();

            return roles;
        }
    }
}
