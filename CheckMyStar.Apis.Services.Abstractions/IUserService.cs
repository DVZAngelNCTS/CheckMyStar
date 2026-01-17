using CheckMyStar.Bll.Models;

namespace CheckMyStar.Apis.Services.Abstractions
{
    public interface IUserService
    {
        Task<List<UserModel>> GetUsers(CancellationToken ct);
    }
}
