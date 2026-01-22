using AutoMapper;

using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data;

namespace CheckMyStar.Bll
{
    public partial class RoleBus(IRoleDal roleDal, IMapper mapper) : IRoleBus
    {
        public async Task<RolesResponse> GetRoles(string? name, CancellationToken ct)
        {
            var roles = await roleDal.GetRoles(name, ct);

            return mapper.Map<RolesResponse>(roles);
        }
        
        public async Task<BaseResponse> AddRole(RoleModel roleModel, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var role = await roleDal.GetRole(roleModel.Identifier, ct);

            if (role.IsSuccess)
            {
                var roleEntity = mapper.Map<Role>(role.Role);

                return mapper.Map<BaseResponse>(await roleDal.AddRole(roleEntity, ct));
            }

            result.IsSuccess = false;
            result.Message = "Le rôle existe déjà, impossible de l'ajouter";

            return result;
        }

        public async Task<BaseResponse> UpdateRole(RoleModel roleModel, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var role = await roleDal.GetRole(roleModel.Identifier, ct);

            if (role.IsSuccess)
            {
                var roleEntity = mapper.Map<Role>(role);

                return mapper.Map<BaseResponse>(await roleDal.UpdateRole(roleEntity, ct));
            }

            result.IsSuccess = false;
            result.Message = "Le rôle n'existe pas, impossible de le modifié";

            return result;
        }

        public async Task<BaseResponse> DeleteRole(int identifier, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var role = await roleDal.GetRole(identifier, ct);

            if (role.IsSuccess)
            {
                var roleEntity = mapper.Map<Role>(role);

                return mapper.Map<BaseResponse>(await roleDal.DeleteRole(roleEntity, ct));
            }

            result.IsSuccess = false;
            result.Message = "Le rôle n'existe pas, impossible de le supprimer";

            return result;
        }
    }
}
