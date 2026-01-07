using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Models;

namespace CheckMyStar.Apis.Services
{
    public class UserService(IUserBusForService userBusForService) : IUserService
    {
        public async Task<List<UserModel>> GetUsers()
        {
            var users = await userBusForService.GetUsersForService();

            return users;
        }
    }
}
