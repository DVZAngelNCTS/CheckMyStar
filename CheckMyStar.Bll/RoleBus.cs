using AutoMapper;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data;

namespace CheckMyStar.Bll
{
    public partial class RoleBus(IUserContextService userContext, IActivityBus activityBus, IRoleDal roleDal, IMapper mapper) : IRoleBus
    {
        public async Task<RoleResponse> GetIdentifier(CancellationToken ct)
        {
            var role = await roleDal.GetNextIdentifier(ct);

            return mapper.Map<RoleResponse>(role);
        }

        public async Task<RolesResponse> GetRoles(string? name, CancellationToken ct)
        {
            var roles = await roleDal.GetRoles(name, ct);

            return mapper.Map<RolesResponse>(roles);
        }

        public async Task<BaseResponse> AddRole(RoleModel roleModel, int currentUser, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var role = await roleDal.GetRole(roleModel.Identifier, ct);

            if (role.IsSuccess)
            {
                if (role.Role == null)
                {
                    role = await roleDal.GetRole(roleModel.Name, ct);

                    if (role.IsSuccess)
                    {
                        if (role.Role == null)
                        {
                            var dateTime = DateTime.Now;

                            var roleEntity = mapper.Map<Role>(roleModel);

                            result = mapper.Map<BaseResponse>(await roleDal.AddRole(roleEntity, ct));

                            await activityBus.AddActivity(result.Message, dateTime, currentUser, result.IsSuccess, ct);
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Message = "Le nom du rôle existe déjà";
                        }
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = role.Message;
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "L'identifiant du rôle existe déjà";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = role.Message;
            }

            return result;
        }

        public async Task<BaseResponse> UpdateRole(RoleModel roleModel, int currentUser, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var role = await roleDal.GetRole(roleModel.Identifier, ct);

            if (role.IsSuccess)
            {
                if (role.Role != null)
                {
                    var dateTime = DateTime.Now;

                    roleModel.CreatedDate = role.Role.CreatedDate;
                    roleModel.UpdatedDate = dateTime;

                    var roleEntity = mapper.Map<Role>(roleModel);

                    result = mapper.Map<BaseResponse>(await roleDal.UpdateRole(roleEntity, ct));

                    await activityBus.AddActivity(result.Message, dateTime, currentUser, result.IsSuccess, ct);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Le rôle n'existe pas, impossible de le modifié";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = role.Message;
            }

            return result;
        }

        public async Task<BaseResponse> DeleteRole(int identifier, int currentUser, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var role = await roleDal.GetRole(identifier, ct);

            if (role.IsSuccess)
            {
                if (role.Role != null)
                {
                    var roleEntity = mapper.Map<Role>(role.Role);

                    result = mapper.Map<BaseResponse>(await roleDal.DeleteRole(roleEntity, ct));

                    await activityBus.AddActivity(result.Message, DateTime.Now, currentUser, result.IsSuccess, ct);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Le rôle n'existe pas, impossible de le supprimer";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = role.Message;
            }

            return result;
        }

        public async Task<BaseResponse> EnabledRole(int identifier, bool isActive, int currentUser, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var user = await roleDal.GetRole(identifier, ct);

            if (user.IsSuccess)
            {
                if (user.Role != null)
                {
                    user.Role.IsActive = isActive;

                    var roleResult = await roleDal.EnabledRole(user.Role, ct);

                    if (roleResult.IsSuccess)
                    {
                        result.IsSuccess = true;
                        result.Message = roleResult.Message;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = roleResult.Message;
                    }

                    await activityBus.AddActivity(roleResult.Message, DateTime.Now, currentUser, roleResult.IsSuccess, ct);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = $"Le rôle n'existe pas, impossible de {(isActive == true ? "l'activer" : "le désactiver")}";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = user.Message;
            }

            return result;
        }
    }
}
