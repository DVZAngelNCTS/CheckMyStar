using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Requests;

namespace CheckMyStar.Apis.Services.Abstractions
{
    public interface IRoleService
    {
        Task<List<RoleModel>> GetRoles(RoleGetRequest request, CancellationToken ct);
    }
}
