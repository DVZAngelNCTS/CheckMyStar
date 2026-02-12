using System.Data;

using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Models;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal
{
    public class CriteriaDal(ICheckMyStarDbContext dbContext) : ICriteresDal
    {
        public async Task<CriterionResult> GetNextIdentifier(CancellationToken ct)
        {
            CriterionResult criterionResult = new CriterionResult();

            try
            {
                var existingIdentifiers = await (from r in dbContext.Criterias.AsNoTracking()
                                                 orderby r.CriterionId
                                                 select r.CriterionId).ToListAsync(ct);

                if (existingIdentifiers.Count == 0)
                {
                    criterionResult.IsSuccess = true;
                    criterionResult.Criterion = new Criterion { CriterionId = 1 };
                    criterionResult.Message = "Identifiant récupéré avec succès";
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

                    criterionResult.IsSuccess = true;
                    criterionResult.Criterion = new Criterion { CriterionId = nextIdentifier };
                    criterionResult.Message = "Identifiant récupéré avec succès";
                }
            }
            catch (Exception ex)
            {
                criterionResult.IsSuccess = false;
                criterionResult.Message = ex.Message;
            }

            return criterionResult;
        }

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

        public async Task<int> CreateCriterionAsync(string description, decimal basePoints, CancellationToken ct)
        {
            try
            {
                var criterion = new Criterion
                {
                    Description = description,
                    BasePoints = basePoints
                };

                await dbContext.AddAsync(criterion, ct);
                await dbContext.SaveChangesAsync(ct);

                return criterion.CriterionId;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the criterion.", ex);
            }
        }

        public async Task<BaseResult> AddStarLevelCriterion(StarLevelCriterion starLevelCriterion , CancellationToken ct)
        {
            BaseResult baseResult = new BaseResult();

            try
            {
                await dbContext.AddAsync(starLevelCriterion, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    baseResult.IsSuccess = true;
                    baseResult.Message = $"Niveau de critère {starLevelCriterion.TypeCode} ajouté avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = $"Impossible d'ajouter le niveau de critère {starLevelCriterion.TypeCode}";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = $"Impossible d'ajouter le niveau de critère {starLevelCriterion.TypeCode} : " + ex.Message;
            }

            return baseResult;
        }

        public async Task<BaseResult> AddCriterion(Criterion criterion, CancellationToken ct)
        {
            BaseResult baseResult = new BaseResult();

            try
            {
                await dbContext.AddAsync(criterion, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    baseResult.IsSuccess = true;
                    baseResult.Message = $"Critère {criterion.Description} ajouté avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = $"Impossible d'ajouter le critère {criterion.Description}";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = $"Impossible d'ajouter le critère {criterion.Description} : " + ex.Message;
            }

            return baseResult;
        }
    }
}