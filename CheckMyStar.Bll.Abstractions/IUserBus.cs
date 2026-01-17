using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IUserBus
    {
        Task<UserModel?> GetUser(string login, string password, CancellationToken ct);
        Task<List<UserModel>> GetUsers(CancellationToken ct);
    }
}
