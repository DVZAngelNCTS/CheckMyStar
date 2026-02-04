using System.Data;

using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal
{
    public class RoleDal(ICheckMyStarDbContext dbContext) : IRoleDal
    {
        public async Task<RolesResult> GetRoles(string? name, CancellationToken ct)
        {
            RolesResult rolesResult = new RolesResult();

            try
            {
                var roles = await (from r in dbContext.Roles
                               where
                                string.IsNullOrEmpty(name) || r.Name.Contains(name)
                               orderby r.Name
                               select r).AsNoTracking().ToListAsync(ct);

                rolesResult.Roles = roles;
                rolesResult.IsSuccess = true;
            }
            catch (Exception ex)
            {
                rolesResult.IsSuccess = true;
                rolesResult.Message = ex.Message;
            }

            return rolesResult;
        }

        public async Task<RoleResult> GetRole(int identifier, CancellationToken ct)
        {
            RoleResult roleResult = new RoleResult();

            try
            {
                var role = await (from r in dbContext.Roles
                                   where
                                    r.Identifier == identifier                                   
                                   select r).AsNoTracking().FirstOrDefaultAsync(ct);

                roleResult.Role = role;
                roleResult.IsSuccess = true;
            }
            catch (Exception ex)
            {
                roleResult.IsSuccess = true;
                roleResult.Message = ex.Message;
            }

            return roleResult;
        }

        public async Task<RoleResult> GetRole(string name, CancellationToken ct)
        {
            RoleResult roleResult = new RoleResult();

            try
            {
                var role = await (from r in dbContext.Roles
                                  where
                                   r.Name.ToLower() == name.ToLower()
                                  select r).AsNoTracking().FirstOrDefaultAsync(ct);

                roleResult.Role = role;
                roleResult.IsSuccess = true;
            }
            catch (Exception ex)
            {
                roleResult.IsSuccess = true;
                roleResult.Message = ex.Message;
            }

            return roleResult;
        }

        public async Task<BaseResult> AddRole(Role role, CancellationToken ct)
        {
            BaseResult baseResult = new BaseResult();

            try
            {
                await dbContext.AddAsync(role, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    baseResult.IsSuccess = true;
                    baseResult.Message = "Rôle ajouté avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = "Impossible d'ajouter le rôle";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = "Impossible d'ajouter le rôle : " + ex.Message;
            }

            return baseResult;
        }

        public async Task<BaseResult> UpdateRole(Role role, CancellationToken ct)
        {
            BaseResult baseResult = new BaseResult();

            try
            {
                await dbContext.UpdateAsync(role, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    baseResult.IsSuccess = true;
                    baseResult.Message = "Rôle modifié avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = "Impossible de modifier le rôle";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = "Impossible de modifier le rôle : " + ex.Message;
            }

            return baseResult;
        }

        public async Task<BaseResult> DeleteRole(Role role, CancellationToken ct)
        {
            BaseResult baseResult = new BaseResult();

            try
            {
                await dbContext.RemoveAsync(role, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    baseResult.IsSuccess = true;
                    baseResult.Message = "Rôle supprimé avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = "Impossible de supprimer le rôle";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = "Impossible de supprimer le rôle : " + ex.Message;
            }

            return baseResult;
        }
    }
}
