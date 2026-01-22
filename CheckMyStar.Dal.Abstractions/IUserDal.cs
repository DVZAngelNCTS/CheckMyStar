using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IUserDal
    {
        Task<UserResult> GetUser(string login, string password, CancellationToken ct);
        Task<List<User>> GetUsers(CancellationToken ct);
    }
}
