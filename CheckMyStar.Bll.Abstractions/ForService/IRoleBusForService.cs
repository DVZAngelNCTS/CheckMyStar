using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Requests;

namespace CheckMyStar.Bll.Abstractions.ForService
{
    public interface IRoleBusForService
    {
        Task<List<RoleModel>> GetRoles(RoleGetRequest request, CancellationToken ct);
    }
}
