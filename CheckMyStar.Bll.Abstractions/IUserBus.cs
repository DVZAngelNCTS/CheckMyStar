using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IUserBus
    {
        Task<UserResponse> GetUser(string login, string password, CancellationToken ct);
        Task<UsersResponse> GetUsers(string lastName, string firstName, string society, string email, string phone, CancellationToken ct);
    }
}
