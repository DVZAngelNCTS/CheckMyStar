using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll
{
    public partial class RoleBus : IRoleBusForService
    {
        public Task<RolesResponse> GetRoles(RoleGetRequest request, CancellationToken ct)
        {
            return this.GetRoles(request.Name, ct);
        }

        public Task<BaseResponse> AddRole(RoleSaveRequest request, CancellationToken ct)
        {
            return this.AddRole(request.Role, ct);
        }

        public Task<BaseResponse> UpdateRole(RoleSaveRequest request, CancellationToken ct)
        {
            return this.UpdateRole(request.Role, ct);
        }

        public Task<BaseResponse> DeleteRole(RoleDeleteRequest request, CancellationToken ct)
        {
            return this.DeleteRole(request.Identifier, ct);
        }
    }
}
