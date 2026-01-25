using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services
{
    public class UserService(IUserBusForService userBusForService) : IUserService
    {
        public async Task<UsersResponse> GetUsers(UserGetRequest request, CancellationToken ct)
        {
            var users = await userBusForService.GetUsers(request, ct);

            return users;
        }
    }
}
