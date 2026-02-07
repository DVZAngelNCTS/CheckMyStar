using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services
{
    public class UserService(IUserBusForService userBusForService) : IUserService
    {
        public async Task<UserResponse> GetNextIdentifier(CancellationToken ct)
        {
            var user = await userBusForService.GetNextIdentifier(ct);

            return user;
        }

        public async Task<UsersResponse> GetUsers(UserGetRequest request, CancellationToken ct)
        {
            var users = await userBusForService.GetUsers(request, ct);

            return users;
        }

        public async Task<BaseResponse> AddUser(UserSaveRequest request, CancellationToken ct)
        {
            var result = await userBusForService.AddUser(request, ct);

            return result;
        }

        public async Task<BaseResponse> UpdateUser(UserSaveRequest request, CancellationToken ct)
        {
            var result = await userBusForService.UpdateUser(request, ct);

            return result;
        }

        public async Task<BaseResponse> DeleteUser(UserDeleteRequest request, CancellationToken ct)
        {
            var result = await userBusForService.DeleteUser(request, ct);

            return result;
        }

    }
}
