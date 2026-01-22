using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services
{
    public class RoleService(IRoleBusForService roleBusForService) : IRoleService
    {
        public async Task<RolesResponse> GetRoles(RoleGetRequest request, CancellationToken ct)
        {
            var result = await roleBusForService.GetRoles(request, ct);

            return result;
        }

        public async Task<BaseResponse> AddRole(RoleSaveRequest request, CancellationToken ct)
        {
            var result = await roleBusForService.AddRole(request, ct);

            return result;
        }

        public async Task<BaseResponse> UpdateRole(RoleSaveRequest request, CancellationToken ct)
        {
            var result = await roleBusForService.UpdateRole(request, ct);

            return result;
        }

        public async Task<BaseResponse> DeleteRole(RoleDeleteRequest request, CancellationToken ct)
        {
            var result = await roleBusForService.DeleteRole(request, ct);

            return result;
        }
    }
}
