using Microsoft.EntityFrameworkCore;

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
                                  select u).AsNoTracking().FirstOrDefaultAsync(ct);

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

        public async Task<UsersResult> GetUsers(string lastName, string firstName, string society, string email, string phone, string address, int? role, CancellationToken ct)
        {
            UsersResult userResult = new UsersResult();

            try
            {
                var users = await (from u in dbContext.Users
                                   join a in dbContext.Addresses on u.AddressIdentifier equals a.Identifier into ua
                                   from a in ua.DefaultIfEmpty()
                                   where
                                      (string.IsNullOrEmpty(lastName) || u.LastName.Contains(lastName))
                                   && (string.IsNullOrEmpty(firstName) || u.LastName.Contains(firstName))
                                   && (string.IsNullOrEmpty(society) || u.LastName.Contains(society))
                                   && (string.IsNullOrEmpty(email) || u.LastName.Contains(email))
                                   && (string.IsNullOrEmpty(phone) || u.LastName.Contains(phone))
                                   && (role == null || u.RoleIdentifier == role)
                                   && (string.IsNullOrEmpty(address) || a.Number.Contains(address)
                                   || a.AddressLine.Contains(address)
                                   || a.City.Contains(address)
                                   || a.ZipCode.Contains(address))
                                   select u).AsNoTracking().ToListAsync(ct);

                userResult.IsSuccess = true;
                userResult.Users = users;
            }
            catch (Exception ex)
            {
                userResult.IsSuccess = false;
                userResult.Message = ex.Message;
            }

            return userResult;
        }
    }
}
