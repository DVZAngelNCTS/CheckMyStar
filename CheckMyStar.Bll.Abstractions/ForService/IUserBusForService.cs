using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService
{
    public interface IUserBusForService
    {
        Task<UserResponse> GetNextIdentifier(CancellationToken ct);
        Task<UserResponse> GetUser(LoginGetRequest request, CancellationToken ct);
        Task<UsersResponse> GetUsers(UserGetRequest request, CancellationToken ct);
        Task<BaseResponse> AddUser(UserSaveRequest request, CancellationToken ct);
        Task<BaseResponse> UpdateUser(UserSaveRequest request, CancellationToken ct);
        Task<BaseResponse> DeleteUser(UserDeleteRequest request, CancellationToken ct);
        Task<UserEvolutionResponse> GetUserEvolutions(CancellationToken ct);
    }
}
