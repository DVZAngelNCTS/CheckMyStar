using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IRoleBus
    {
        Task<RoleResponse> GetIdentifier(CancellationToken ct);
        Task<RolesResponse> GetRoles(string? name, CancellationToken ct);
        Task<BaseResponse> AddRole(RoleModel roleModel, CancellationToken ct);
        Task<BaseResponse> UpdateRole(RoleModel roleModel, CancellationToken ct);
        Task<BaseResponse> DeleteRole(int identifier, CancellationToken ct);
    }
}
