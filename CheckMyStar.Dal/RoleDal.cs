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
        public async Task<RoleResult> GetNextIdentifier(CancellationToken ct)
        {
            RoleResult roleResult = new RoleResult();

            try
            {
                var existingIdentifiers = await (from r in dbContext.Roles.AsNoTracking()
                                                 orderby r.Identifier
                                                 select r.Identifier).ToListAsync(ct);

                if (existingIdentifiers.Count == 0)
                {
                    roleResult.IsSuccess = true;
                    roleResult.Role = new Role { Identifier = 1 };
                    roleResult.Message = "Identifiant récupéré avec succès";
                }
                else
                {
                    int nextIdentifier = 1;

                    foreach (var id in existingIdentifiers)
                    {
                        if (id == nextIdentifier)
                        {
                            nextIdentifier++;
                        }
                        else if (id > nextIdentifier)
                        {
                            break;
                        }
                    }

                    roleResult.IsSuccess = true;
                    roleResult.Role = new Role { Identifier = nextIdentifier };
                    roleResult.Message = "Identifiant récupéré avec succès";
                }
            }
            catch (Exception ex)
            {
                roleResult.IsSuccess = false;
                roleResult.Message = ex.Message;
            }

            return roleResult;
        }

        public async Task<RolesResult> GetRoles(string? name, CancellationToken ct)
        {
            RolesResult rolesResult = new RolesResult();

            try
            {
                var roles = await (from r in dbContext.Roles.AsNoTracking()
                                   where
                                string.IsNullOrEmpty(name) || r.Name.Contains(name)
                               orderby r.Name
                               select r).ToListAsync(ct);

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
                var role = await (from r in dbContext.Roles.AsNoTracking()
                                  where
                                    r.Identifier == identifier                                   
                                   select r).FirstOrDefaultAsync(ct);

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
                var role = await (from r in dbContext.Roles.AsNoTracking()
                                  where
                                   r.Name.ToLower() == name.ToLower()
                                  select r).FirstOrDefaultAsync(ct);

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
                    baseResult.Message = $"Rôle {role.Name} ajouté avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = $"Impossible d'ajouter le rôle {role.Name}";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = $"Impossible d'ajouter le rôle {role.Name} : " + ex.Message;
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
                    baseResult.Message = $"Rôle {role.Name} modifié avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = $"Impossible de modifier le rôle {role.Name}";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = $"Impossible de modifier le rôle {role.Name} : " + ex.Message;
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
                    baseResult.Message = $"Rôle {role.Name} supprimé avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = $"Impossible de supprimer le rôle {role.Name}";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = $"Impossible de supprimer le rôle {role.Name} : " + ex.Message;
            }

            return baseResult;
        }
    }
}
