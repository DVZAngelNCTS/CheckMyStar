using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IRoleDal
    {
        Task<RoleResult> GetNextIdentifier(CancellationToken ct);
        Task<RolesResult> GetRoles(string? name, CancellationToken ct);
        Task<RoleResult> GetRole(int identifier, CancellationToken ct);
        Task<RoleResult> GetRole(string name, CancellationToken ct);
        Task<BaseResult> AddRole(Role role, CancellationToken ct);
        Task<BaseResult> UpdateRole(Role role, CancellationToken ct);
        Task<BaseResult> DeleteRole(Role role, CancellationToken ct);
    }
}
