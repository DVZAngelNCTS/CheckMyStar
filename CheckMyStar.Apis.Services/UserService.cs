using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Models;

namespace CheckMyStar.Apis.Services
{
    public class UserService(IUserBusForService userBusForService) : IUserService
    {
        public async Task<List<UserModel>> GetUsers(CancellationToken ct)
        {
            var users = await userBusForService.GetUsersForService(ct);

            return users;
        }
    }
}
