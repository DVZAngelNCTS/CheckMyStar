using Microsoft.EntityFrameworkCore;

using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;

namespace CheckMyStar.Dal
{
    public class UserDal(ICheckMyStarDbContext dbContext) : IUserDal
    {
        public async Task<UserResult> GetUser(string login, string password, CancellationToken ct)
        {
            UserResult userResult = new UserResult();

            try
            {
                var user = await (from u in dbContext.Users
                                  where
                                      (u.Email == login
                                   || u.LastName == login)
                                   && u.Password == password
                                  select u).FirstOrDefaultAsync(ct);

                userResult.IsSuccess = true;
                userResult.User = user;
            }
            catch (Exception ex)
            {
                userResult.IsSuccess = false;
                userResult.Message = ex.Message;
            }

            return userResult;
        }

        public async Task<List<User>> GetUsers(CancellationToken ct)
        {
            var users = await dbContext.Users.ToListAsync(ct);

            return users;
        }
    }
}
