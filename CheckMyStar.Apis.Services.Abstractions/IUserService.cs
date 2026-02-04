using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services.Abstractions
{
    public interface IUserService
    {
        Task<UsersResponse> GetUsers(UserGetRequest request, CancellationToken ct);
        Task<BaseResponse> AddUser(UserSaveRequest request, CancellationToken ct);
        Task<BaseResponse> UpdateUser(UserSaveRequest request, CancellationToken ct);
        Task<BaseResponse> DeleteUser(UserDeleteRequest request, CancellationToken ct);
    }
}
