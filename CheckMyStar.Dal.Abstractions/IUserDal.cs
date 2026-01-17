using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IUserDal
    {
        Task<User?> GetUser(string login, string password, CancellationToken ct);
        Task<List<User>> GetUsers(CancellationToken ct);
    }
}
