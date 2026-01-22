using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IUserBus
    {
        Task<UserResponse> GetUser(string login, string password, CancellationToken ct);
        Task<List<UserModel>> GetUsers(CancellationToken ct);
    }
}
