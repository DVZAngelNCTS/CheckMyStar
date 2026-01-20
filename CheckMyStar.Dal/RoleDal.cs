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

        //public async Task<bool> SaveRole(Role role)
        //{
        //    if (role == null)
        //        return false;

        //    if (role.Identifier == 0)
        //    {
        //        await dbContext.Roles.AddAsync(role);
        //    }
        //    else
        //    {
        //        var existing = await dbContext.Roles.FirstOrDefaultAsync(r => r.Identifier == role.Identifier);

        //        if (existing == null)
        //            return false;

        //        existing.Name = role.Name;
        //    }

        //    await dbContext.SaveChangesAsync();

        //    return true;
        //}

    }
}
