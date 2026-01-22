using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService
{
    public interface IUserBusForService
    {
        Task<UserResponse> GetUser(LoginGetRequest request, CancellationToken ct);
        Task<List<UserModel>> GetUsersForService(CancellationToken ct);
    }
}
