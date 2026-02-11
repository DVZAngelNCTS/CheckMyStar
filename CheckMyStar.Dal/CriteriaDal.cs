using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Models;
using CheckMyStar.Data.Abstractions;
using CheckMyStar.Dal.Results;

namespace CheckMyStar.Dal
{
    public class CriteriaDal(ICheckMyStarDbContext dbContext) : ICriteresDal
    {
        public async Task<StarCriteriasDetailResult> GetStarCriteriaDetails(CancellationToken ct)
        {
            StarCriteriasDetailResult starCriteriaDetailResult = new StarCriteriasDetailResult();

            try
            {
                var query = await (from s in dbContext.StarLevels.AsNoTracking()
                                   join slc in dbContext.StarLevelCriterias.AsNoTracking() on s.StarLevelId equals slc.StarLevelId
                                   join c in dbContext.Criterias.AsNoTracking() on slc.CriterionId equals c.CriterionId
                                   join crt in dbContext.CriterionTypes.AsNoTracking() on slc.TypeCode equals crt.TypeCode
                                   orderby s.StarLevelId, c.CriterionId
                                   select new StarCriteriaDetail
                                   {
                                       Rating = Convert.ToInt32(s.StarLevelId),
                                       StarLabel = s.Label,
                                       CriterionId = c.CriterionId,
                                       Description = c.Description,
                                       BasePoints = c.BasePoints,
                                       TypeCode = slc.TypeCode,
                                       TypeLabel = crt.Label
                                   }).ToListAsync(ct);

                starCriteriaDetailResult.IsSuccess = true;
                starCriteriaDetailResult.StarCriteriaDetails = query;
            }
            catch (Exception ex)
            {
                starCriteriaDetailResult.IsSuccess = false;
                starCriteriaDetailResult.Message = ex.Message;
            }

            return starCriteriaDetailResult;
        }

        public async Task<StarCriteriasResult> GetStarCriterias(CancellationToken ct)
        {
            StarCriteriasResult starCriteriaResult = new StarCriteriasResult();

            try
            {
                var query = await (from s in dbContext.StarLevels.AsNoTracking()
                                   join slc in dbContext.StarLevelCriterias.AsNoTracking() on s.StarLevelId equals slc.StarLevelId
                                   join crt in dbContext.CriterionTypes.AsNoTracking() on slc.TypeCode equals crt.TypeCode
                                   group new { s, ct } by new { Rating = s.StarLevelId, StarLabel = s.Label, crt.TypeCode, TypeLabel = crt.Label } into g
                                   orderby g.Key.Rating, g.Key.TypeCode
                                   select new StarCriteria
                                   {
                                       Rating = Convert.ToInt32(g.Key.Rating),
                                       StarLabel = g.Key.StarLabel ?? string.Empty,
                                       TypeCode = g.Key.TypeCode ?? string.Empty,
                                       TypeLabel = g.Key.TypeLabel ?? string.Empty,
                                       Count = g.Count()
                                   }).ToListAsync();

                starCriteriaResult.IsSuccess = true;
                starCriteriaResult.StarCriterias = query;
            }
            catch (Exception ex)
            {
                starCriteriaResult.IsSuccess = false;
                starCriteriaResult.Message = ex.Message;
            }

            return starCriteriaResult;
        }
    }
}
