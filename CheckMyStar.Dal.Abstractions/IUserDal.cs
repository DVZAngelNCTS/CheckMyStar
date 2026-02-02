using CheckMyStar.Dal.Results;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IUserDal
    {
        Task<UserResult> GetUser(string login, string password, CancellationToken ct);
        Task<UsersResult> GetUsers(string lastName, string firstName, string society, string email, string phone, string address, int? role, CancellationToken ct);
    }
}
