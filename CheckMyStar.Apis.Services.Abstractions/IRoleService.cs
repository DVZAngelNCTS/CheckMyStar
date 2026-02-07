using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services.Abstractions
{
    public interface IRoleService
    {
        Task<RoleResponse> GetNextIdentifier(CancellationToken ct);
        Task<RolesResponse> GetRoles(RoleGetRequest request, CancellationToken ct);
        Task<BaseResponse> AddRole(RoleSaveRequest request, CancellationToken ct);
        Task<BaseResponse> UpdateRole(RoleSaveRequest request, CancellationToken ct);
        Task<BaseResponse> DeleteRole(RoleDeleteRequest request, CancellationToken ct);
    }
}
