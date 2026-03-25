using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services.Abstractions
{
    public interface IUserService
    {
        Task<UserResponse> GetNextIdentifier(CancellationToken ct);
        Task<UserResponse> GetUser(UserGetByIdentifierRequest request, CancellationToken ct);
        Task<UsersResponse> GetUsers(UserGetRequest request, CancellationToken ct);
        Task<UserResponse> GetUser(EmailGetRequest request, CancellationToken ct);
        Task<UserResponse> GetUser(UserGetRequest request, CancellationToken ct);
        Task<BaseResponse> AddUser(UserSaveRequest request, CancellationToken ct);
        Task<BaseResponse> UpdateUser(UserSaveRequest request, CancellationToken ct);
        Task<BaseResponse> DeleteUser(UserDeleteRequest request, CancellationToken ct);
        Task<BaseResponse> EnabledUser(UserSaveRequest request, CancellationToken ct);
        Task<UserEvolutionResponse> GetUserEvolutions(CancellationToken ct);
    }
}
