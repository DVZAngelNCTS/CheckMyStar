using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Requests;

namespace CheckMyStar.Apis.Services
{
    public class RoleService(IRoleBusForService roleBusForService) : IRoleService
    {
        public async Task<List<RoleModel>> GetRoles(RoleGetRequest request, CancellationToken ct)
        {
            var users = await roleBusForService.GetRoles(request, ct);

            return users;
        }
    }
}
