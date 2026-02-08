using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Data;

namespace CheckMyStar.Bll
{
    public partial class RoleBus : IRoleBusForService
    {
        public Task<RoleResponse> GetNextIdentifier(CancellationToken ct)
        {
            return this.GetIdentifier(ct);
        }

        public Task<RolesResponse> GetRoles(RoleGetRequest request, CancellationToken ct)
        {
            return this.GetRoles(request.Name, ct);
        }

        public Task<BaseResponse> AddRole(RoleSaveRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.AddRole(request.Role, user, ct);
        }

        public Task<BaseResponse> UpdateRole(RoleSaveRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.UpdateRole(request.Role, user, ct);
        }

        public Task<BaseResponse> DeleteRole(RoleDeleteRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.DeleteRole(request.Identifier, user, ct);
        }
    }
}
