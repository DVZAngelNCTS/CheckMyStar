using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Requests;

namespace CheckMyStar.Bll
{
    public partial class UserBus : IUserBusForService
    {
        public Task<UserModel?> GetUser(LoginGetRequest request, CancellationToken ct)
        {
            return this.GetUser(request.Login, request.Password, ct);
        }

        public Task<List<UserModel>> GetUsersForService(CancellationToken ct)
        {
            var user = userContextService.CurrentUser;

            return this.GetUsers(ct);
        }
    }
}
