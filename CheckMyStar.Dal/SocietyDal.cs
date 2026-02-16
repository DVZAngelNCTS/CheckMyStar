using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CheckMyStar.Dal;

public class SocietyDal(ICheckMyStarDbContext dbContext) : ISocietyDal
{
    public async Task<SocietyResult> AddSociety(Society society, CancellationToken ct)
    {
        var result = new SocietyResult();
        try
        {
            society.CreatedDate = DateTime.UtcNow;
            society.UpdatedDate = DateTime.UtcNow;
            society.IsActive = true;

            await dbContext.AddAsync(society, ct);
            var affected = await dbContext.SaveChangesAsync(ct);

            if (affected > 0)
            {
                result.IsSuccess = true;
                result.Message = "Société créée avec succès.";
                result.SocietyId = society.Identifier;
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
}