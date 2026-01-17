using Microsoft.EntityFrameworkCore;

using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;
using CheckMyStar.Dal.Abstractions;

namespace CheckMyStar.Dal
{
    public class UserDal(ICheckMyStarDbContext dbContext) : IUserDal
    {
        public async Task<User?> GetUser(string login, string password, CancellationToken ct)
        {
            var user = await (from u in dbContext.Users
                              where
                                  (u.Email == login
                               || u.LastName == login)
                               && u.Password == password
                              select u).FirstOrDefaultAsync(ct);

            return user;
        }

        public async Task<List<User>> GetUsers(CancellationToken ct)
        {
            var users = await dbContext.Users.ToListAsync(ct);

            return users;
        }
    }
}
