using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Requests;

namespace CheckMyStar.Bll
{
    public partial class UserBus : IUserBusForService
    {
        public Task<UserModel?> GetUser(LoginGetRequest request)
        {
            return this.GetUser(request.Login, request.Password);
        }
    }
}
