using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services.Abstractions
{
    public interface IUserService
    {
        Task<UsersResponse> GetUsers(UserGetRequest request, CancellationToken ct);
    }
}
