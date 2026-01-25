using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService
{
    public interface IUserBusForService
    {
        Task<UserResponse> GetUser(LoginGetRequest request, CancellationToken ct);
        Task<UsersResponse> GetUsers(UserGetRequest request, CancellationToken ct);
    }
}
