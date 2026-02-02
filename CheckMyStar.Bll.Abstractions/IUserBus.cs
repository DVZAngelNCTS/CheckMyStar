using CheckMyStar.Bll.Responses;
using CheckMyStar.Enumerations;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IUserBus
    {
        Task<UserResponse> GetUser(string login, string password, CancellationToken ct);
        Task<UsersResponse> GetUsers(string lastName, string firstName, string society, string email, string phone, string address, int? role, CancellationToken ct);
    }
}
