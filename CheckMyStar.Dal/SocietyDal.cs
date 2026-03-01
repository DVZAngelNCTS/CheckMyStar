using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace CheckMyStar.Dal;

public class SocietyDal(ICheckMyStarDbContext dbContext) : ISocietyDal
{
    public async Task<SocietyResult> AddSociety(Society society, CancellationToken ct)
    {
        var result = new SocietyResult();

        try
        {
            society.CreatedDate = DateTime.Now;
            society.UpdatedDate = DateTime.Now;
            society.IsActive = true;

            await dbContext.AddAsync(society, ct);

            var affected = await dbContext.SaveChangesAsync(ct);

            if (affected > 0)
            {
                result.IsSuccess = true;
                result.Message = "Société créée avec succès.";
                result.Society = society;
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "Échec de la création de la société.";
            }
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.Message = $"Erreur lors de la création : {ex.Message}";
        }

        return result;
    }

    public async Task<BaseResult> DeleteSocity(Society society, CancellationToken ct)
    {
        var result = new BaseResult();

        try
        {
            await dbContext.RemoveAsync(society, ct);

            var affected = await dbContext.SaveChangesAsync(ct);

            if (affected > 0)
            {
                result.IsSuccess = true;
                result.Message = "Société supprimée avec succès.";
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "Échec de la suppression de la société.";
            }
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.Message = $"Erreur lors de la suppression : {ex.Message}";
        }

        return (result);
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

    public async Task<SocietiesResult> GetSocieties(CancellationToken ct)
    {
        var result = new SocietiesResult();

        try
        {
            var societies = await dbContext.Societies.AsNoTracking().ToListAsync(ct);

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