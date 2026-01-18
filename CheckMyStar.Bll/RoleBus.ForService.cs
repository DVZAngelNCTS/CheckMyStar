using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll
{
    public partial class RoleBus : IRoleBusForService
    {
        public Task<List<RoleModel>> GetRoles(RoleGetRequest request, CancellationToken ct)
        {
            return this.GetRoles(request.Name, ct);
        }
    }
}
