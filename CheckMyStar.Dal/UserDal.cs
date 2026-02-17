using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Models;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal
{
    public class UserDal(ICheckMyStarDbContext dbContext) : IUserDal
    {
        public async Task<UserResult> GetNextIdentifier(CancellationToken ct)
        {
            UserResult userResult = new UserResult();

            try
            {
                var existingIdentifiers = await (from r in dbContext.Users.AsNoTracking()
                                                 orderby r.Identifier
                                                 select r.Identifier).ToListAsync(ct);

                if (existingIdentifiers.Count == 0)
                {
                    userResult.IsSuccess = true;
                    userResult.User = new User { Identifier = 1 };
                    userResult.Message = "Identifiant récupéré avec succès";
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

                    userResult.IsSuccess = true;
                    userResult.User = new User { Identifier = nextIdentifier };
                    userResult.Message = "Identifiant récupéré avec succès";
                }
            }
            catch (Exception ex)
            {
                userResult.IsSuccess = false;
                userResult.Message = ex.Message;
            }

            return userResult;
        }

        public async Task<UserResult> GetUser(string login, string password, CancellationToken ct)
        {
            UserResult userResult = new UserResult();

            try
            {
                var user = await (from u in dbContext.Users.AsNoTracking()
                                  where
                                      (u.Email == login
                                   || u.LastName == login)
                                   && u.Password == password
                                  select u).FirstOrDefaultAsync(ct);

                if (user != null)
                {
                    userResult.IsSuccess = true;
                    userResult.User = user;
                }
                else
                {
                    userResult.IsSuccess = false;
                    userResult.Message = "Utilisateur ou mot de passe incorrect";
                }
            }
            catch (Exception ex)
            {
                userResult.IsSuccess = false;
                userResult.Message = ex.Message;
            }

            return userResult;
        }

        public async Task<UserResult> GetUser(int identifier, CancellationToken ct)
        {
            UserResult userResult = new UserResult();

            try
            {
                var user = await (from r in dbContext.Users.AsNoTracking()
                                  where
                                   r.Identifier == identifier
                                  select r).FirstOrDefaultAsync(ct);


                userResult.IsSuccess = true;
                userResult.User = user;
            }
            catch (Exception ex)
            {
                userResult.IsSuccess = true;
                userResult.Message = ex.Message;
            }

            return userResult;
        }      

        public async Task<UserResult> GetUser(string lastName, string firstName, int? SocietyIdentifier, string email, string? phone, CancellationToken ct)
        {
            UserResult userResult = new UserResult();

            try
            {
                var user = await (from r in dbContext.Users.AsNoTracking()
                                  where
                                      r.LastName == lastName
                                   && r.FirstName == firstName
                                   && (SocietyIdentifier == null || r.SocietyIdentifier == SocietyIdentifier)
                                   && r.Email == email
                                   && (!string.IsNullOrEmpty(phone) && r.Phone == phone)
                                  select r).FirstOrDefaultAsync(ct);


                userResult.IsSuccess = true;
                userResult.User = user;
            }
            catch (Exception ex)
            {
                userResult.IsSuccess = true;
                userResult.Message = ex.Message;
            }

            return userResult;
        }

        public async Task<UsersResult> GetUsers(string lastName, string firstName, int? SocietyIdentifier, string email, string phone, string address, int? role, CancellationToken ct)
        {
            UsersResult userResult = new UsersResult();

            try
            {
                var users = await (from u in dbContext.Users.AsNoTracking()
                                   join a in dbContext.Addresses.AsNoTracking() on u.AddressIdentifier equals a.Identifier into ua
                                   from a in ua.DefaultIfEmpty()
                                   where
                                      (string.IsNullOrEmpty(lastName) || u.LastName.Contains(lastName))
                                   && (string.IsNullOrEmpty(firstName) || u.FirstName.Contains(firstName))
                                   && (SocietyIdentifier == null || u.SocietyIdentifier == SocietyIdentifier)
                                   && (string.IsNullOrEmpty(email) || u.Email.Contains(email))
                                   && (string.IsNullOrEmpty(phone) || (u.Phone != null && u.Phone.Contains(phone)))
                                   && (role == null || u.RoleIdentifier == role)
                                   && (string.IsNullOrEmpty(address) || a.Number.Contains(address)
                                   || a.AddressLine.Contains(address)
                                   || a.City.Contains(address)
                                   || a.ZipCode.Contains(address))
                                   select u).ToListAsync(ct);

                userResult.IsSuccess = true;
                userResult.Users = users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur GetUsers: {ex}");
                userResult.IsSuccess = false;
                userResult.Message = ex.Message;
            }

            return userResult;
        }

        public async Task<BaseResult> AddUser(User user, CancellationToken ct)
        {
            BaseResult baseResult = new BaseResult();

            try
            {
                await dbContext.AddAsync(user, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    baseResult.IsSuccess = true;
                    baseResult.Message = $"Utilisateur {user.LastName} {user.FirstName} ajouté avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = $"Impossible d'ajouter l'utilisateur {user.LastName} {user.FirstName}";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = $"Impossible d'ajouter l'utilisateur {user.LastName} {user.FirstName} : " + ex.Message;
            }

            return baseResult;
        }

        public async Task<BaseResult> UpdateUser(User user, CancellationToken ct)
        {
            BaseResult baseResult = new BaseResult();

            try
            {
                await dbContext.UpdateAsync(user, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    baseResult.IsSuccess = true;
                    baseResult.Message = $"Utilisateur {user.LastName} {user.FirstName} modifié avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = $"Impossible de modifier l'utilisateur {user.LastName} {user.FirstName}";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = $"Impossible de modifier l'utilisateur {user.LastName} {user.FirstName} : " + ex.Message;
            }

            return baseResult;
        }

        public async Task<BaseResult> DeleteUser(User user, CancellationToken ct)
        {
            BaseResult baseResult = new BaseResult();

            try
            {
                await dbContext.RemoveAsync(user, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    baseResult.IsSuccess = true;
                    baseResult.Message = $"Utilisateur {user.LastName} {user.FirstName} supprimé avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = $"Impossible de supprimer l'utilisateur {user.LastName} {user.FirstName}";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = $"Impossible de supprimer l'utilisateur {user.LastName} {user.FirstName} : " + ex.Message;
            }

            return baseResult;
        }

        public async Task<UserEvolutionResult> GetUserEvolutions(CancellationToken ct)
        {
            UserEvolutionResult userEvolutionResult = new UserEvolutionResult();

            try
            {
                var evolutions = await (from u in dbContext.Users
                                        where u.CreatedDate != null
                                        group u by new
                                        {
                                            Year = u.CreatedDate!.Value.Year,
                                            Month = u.CreatedDate.Value.Month
                                        } into g
                                        orderby g.Key.Year, g.Key.Month
                                        select new UserEvolution()
                                        {
                                            Year = g.Key.Year,
                                            Month = g.Key.Month,
                                            Total = g.Count(),
                                            IsActive = g.Count(x => x.IsActive),
                                            IsDisabled = g.Count(x => !x.IsActive)
                                        }).ToListAsync(ct);

                userEvolutionResult.IsSuccess = true;
                userEvolutionResult.Evolutions = evolutions;
            }
            catch (Exception ex)
            {
                userEvolutionResult.IsSuccess = false;
                userEvolutionResult.Message = ex.Message;
            }

            return userEvolutionResult;
        }
    }
}
