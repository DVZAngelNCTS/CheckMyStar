using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CheckMyStar.Dal;

public class SocietyDal(ICheckMyStarDbContext dbContext) : ISocietyDal
{
    public async Task<SocietyResult> GetNextIdentifier(CancellationToken ct)
    {
        SocietyResult societyResult = new SocietyResult();

        try
        {
            var existingIdentifiers = await (from r in dbContext.Societies.AsNoTracking()
                                             orderby r.Identifier
                                             select r.Identifier).ToListAsync(ct);

            if (existingIdentifiers.Count == 0)
            {
                societyResult.IsSuccess = true;
                societyResult.Society = new Society { Identifier = 1 };
                societyResult.Message = "Identifiant récupéré avec succès";
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

                societyResult.IsSuccess = true;
                societyResult.Society = new Society { Identifier = nextIdentifier };
                societyResult.Message = "Identifiant récupéré avec succès";
            }
        }
        catch (Exception ex)
        {
            societyResult.IsSuccess = false;
            societyResult.Message = ex.Message;
        }

        return societyResult;
    }

    public async Task<BaseResult> AddSociety(Society society, CancellationToken ct)
    {
        BaseResult baseResult = new BaseResult();

        try
        {
            await dbContext.AddAsync(society, ct);

            bool result = await dbContext.SaveChangesAsync(ct) > 0 ? true : false;

            if (result)
            {
                baseResult.IsSuccess = true;
                baseResult.Message = $"La société {society.Name} ajoutée avec succès.";
            }
            else
            {
                baseResult.IsSuccess = false;
                baseResult.Message = $"Impossible d'ajouter la société {society.Name}";
            }
        }
        catch (Exception ex)
        {
            baseResult.IsSuccess = false;
            baseResult.Message = $"Impossible d'ajouter la société {society.Name} : " + ex.Message;
        }

        return baseResult;
    }

    public async Task<BaseResult> UpdateSociety(Society society, CancellationToken ct)
    {
        BaseResult baseResult = new BaseResult();

        try
        {
            await dbContext.UpdateAsync(society, ct);

            bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

            if (result)
            {
                baseResult.IsSuccess = true;
                baseResult.Message = $"Société {society.Name} modifié avec succès";
            }
            else
            {
                baseResult.IsSuccess = false;
                baseResult.Message = $"Impossible de modifier la société {society.Name}";
            }
        }
        catch (Exception ex)
        {
            baseResult.IsSuccess = false;
            baseResult.Message = $"Impossible de modifier la société {society.Name} : " + ex.Message;
        }

        return baseResult;
    }

    public async Task<BaseResult> DeleteSociety(Society society, CancellationToken ct)
    {
        BaseResult baseResult = new BaseResult();

        try
        {
            await dbContext.RemoveAsync(society, ct);

            bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

            if (result)
            {
                baseResult.IsSuccess = true;
                baseResult.Message = $"Société {society.Name} supprimé avec succès";
            }
            else
            {
                baseResult.IsSuccess = false;
                baseResult.Message = $"Impossible de supprimer la société {society.Name}";
            }
        }
        catch (Exception ex)
        {
            baseResult.IsSuccess = false;
            baseResult.Message = $"Impossible de supprimer la société {society.Name} : " + ex.Message;
        }

        return baseResult;
    }

    public async Task<BaseResult> EnabledSociety(Society society, CancellationToken ct)
    {
        BaseResult baseResult = new BaseResult();

        try
        {
            dbContext.Entry(society).Property(x => x.IsActive).IsModified = true;

            bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

            if (result)
            {
                baseResult.IsSuccess = true;
                baseResult.Message = $"Société {society.Name} {(society.IsActive == true ? "activé" : "désactivé")} avec succès";
            }
            else
            {
                baseResult.IsSuccess = false;
                baseResult.Message = $"Impossible de {(society.IsActive == true ? "activé" : "désactivé")} la société {society.Name}";
            }
        }
        catch (Exception ex)
        {
            baseResult.IsSuccess = false;
            baseResult.Message = $"Impossible de {(society.IsActive == true ? "activé" : "désactivé")} la société {society.Name} : " + ex.Message;
        }

        return baseResult;
    }

    public async Task<SocietyResult> GetSociety(int identifier, CancellationToken ct)
    {
        var result = new SocietyResult();

        try
        {
            var society = await (from s in dbContext.Societies.AsNoTracking()
                                   where
                                    s.Identifier == identifier
                                   select s).FirstOrDefaultAsync();

            result.IsSuccess = true;
            result.Society = society;
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.Message = ex.Message;
        }

        return result;
    }

    public async Task<SocietyResult> GetSociety(string? name, string? email, string? phone, CancellationToken ct)
    {
        var result = new SocietyResult();

        try
        {
            var society = await (from s in dbContext.Societies.AsNoTracking()
                                 where
                                      s.Name == name
                                   && s.Email == email
                                   && (!string.IsNullOrEmpty(phone) && s.Phone == phone)
                                 select s).FirstOrDefaultAsync();

            result.IsSuccess = true;
            result.Society = society;
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.Message = ex.Message;
        }

        return result;
    }
    public async Task<SocietiesResult> GetSocieties(string? name, string? email, string? phone, string? address, CancellationToken ct)
    {
        var result = new SocietiesResult();

        try
        {
            var societies = await ( from s in dbContext.Societies.AsNoTracking()
                                    join a in dbContext.Addresses.AsNoTracking() on s.AddressIdentifier equals a.Identifier into ua
                                    from a in ua.DefaultIfEmpty()
                                    where
                                       (string.IsNullOrEmpty(name) || s.Name.Contains(name))
                                    && (string.IsNullOrEmpty(email) || (s.Email != null && s.Email.Contains(email)))
                                    && (string.IsNullOrEmpty(phone) || (s.Phone != null && s.Phone.Contains(phone)))
                                    && (string.IsNullOrEmpty(address) || a.Number.Contains(address)
                                    || a.AddressLine.Contains(address)
                                    || a.City.Contains(address)
                                    || a.ZipCode.Contains(address))
                                    select s).ToListAsync(ct);

            result.IsSuccess = true;
            result.Societies = societies;
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.Message = ex.Message;
        }

        return result;
    }
}