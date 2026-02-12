using AutoMapper;

using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Bll
{
    public partial class CriteriaBus(ICriteresDal criteresDal, IMapper mapper) : ICriteriaBus
    {
        public async Task<StarCriteriaDetailsResponse> GetCriteriaDetails(CancellationToken ct)
        {
            StarCriteriaDetailsResponse starCriteriaDetailsResponse = new StarCriteriaDetailsResponse();

            var criteriaDetails = await criteresDal.GetStarCriteriaDetails(ct);

            if (criteriaDetails.IsSuccess && criteriaDetails.StarCriteriaDetails != null)
            {
                var grouped = (criteriaDetails.StarCriteriaDetails
                    .GroupBy(r => new { r.Rating, r.StarLabel })
                    .OrderBy(g => g.Key.Rating)
                    .Select(g => new StarCriteriaModel
                    {
                        Rating = g.Key.Rating,
                        StarLabel = g.Key.StarLabel,
                        Criteria = g.Select(r => new StarCriteriaDetailModel
                        {
                            CriterionId = r.CriterionId,
                            Description = r.Description,
                            BasePoints = r.BasePoints,
                            TypeCode = r.TypeCode,
                            TypeLabel = r.TypeLabel
                        }).OrderBy(c => c.CriterionId).ToList()
                    })).ToList();

                starCriteriaDetailsResponse.IsSuccess = true;
                starCriteriaDetailsResponse.StarCriterias = grouped;
            }
            else
            {
                starCriteriaDetailsResponse.IsSuccess = false;
                starCriteriaDetailsResponse.Message = criteriaDetails.Message;
            }

            return starCriteriaDetailsResponse;
        }

        public async Task<StarCriteriaStatusResponse> GetCriteriaStatus(CancellationToken ct)
        {
            StarCriteriaStatusResponse starCriteriasResponse = new StarCriteriaStatusResponse();

            var criterias = await criteresDal.GetStarCriterias(ct);

            if (criterias.IsSuccess && criterias.StarCriterias != null)
            {
                var grouped = (criterias.StarCriterias
                    .GroupBy(r => new { r.Rating, r.StarLabel })
                    .OrderBy(g => g.Key.Rating)
                    .Select(g => new StarCriteriaStatusModel
                    {
                        Rating = g.Key.Rating,
                        Label = g.Key.StarLabel,
                        Description = $"Critères {g.Key.Rating} étoile(s)",
                        LastUpdate = DateTime.UtcNow,
                        Statuses = g.Select(r => new StarStatusModel
                        {
                            Code = r.TypeCode,
                            Label = r.TypeLabel,
                            Count = r.Count
                        }).OrderBy(s => s.Code).ToList()
                    })).ToList();

                starCriteriasResponse.IsSuccess = true;
                starCriteriasResponse.StarCriterias = grouped;
            }
            else
            {
                starCriteriasResponse.IsSuccess = false;
                starCriteriasResponse.Message = criterias.Message;
            }

            return starCriteriasResponse;
        }

        public async Task<BaseResponse> AddCriterion(StarCriterionModel starCriterionModel, StarLevelCriterionModel starLevelCriterionModel, CancellationToken ct)
        {
            BaseResponse baseResponse = new BaseResponse();

            try
            {
                CriterionResult criterionResult = await criteresDal.GetNextIdentifier(ct);

                if (!criterionResult.IsSuccess && criterionResult.Criterion != null)
                {
                    var starCriterion = mapper.Map<Criterion>(starCriterionModel);

                    starCriterion.CriterionId = criterionResult.Criterion.CriterionId;

                    BaseResult baseResultCriterion = await criteresDal.AddCriterion(starCriterion, ct);

                    if (baseResultCriterion.IsSuccess)
                    {
                        var starLevelCriterion = mapper.Map<StarLevelCriterion>(starLevelCriterionModel);

                        starLevelCriterion.CriterionId = starCriterion.CriterionId;

                        BaseResult baseResultLevelCriterion = await criteresDal.AddStarLevelCriterion(starLevelCriterion, ct);

                        if (baseResultLevelCriterion.IsSuccess)
                        {
                            baseResponse.IsSuccess = true;
                            baseResponse.Message = baseResultCriterion.Message + "<br>" + baseResultLevelCriterion.Message;
                        }
                        else
                        {
                            baseResponse.IsSuccess = false;
                            baseResponse.Message = baseResultCriterion.Message + "<br>" + baseResultLevelCriterion.Message;
                        }
                    }
                    else
                    {
                        baseResponse.IsSuccess = false;
                        baseResponse.Message = baseResultCriterion.Message;
                    }
                }
                else
                {
                    baseResponse.IsSuccess = false;
                    baseResponse.Message = criterionResult.Message;

                }
            }
            catch (Exception ex)
            {
                baseResponse.IsSuccess = false;
                baseResponse.Message = ex.Message;
            }

            return baseResponse;
        }

        public async Task<BaseResponse> DeleteCriterion(int criterionId, CancellationToken ct)
        {
            var response = new BaseResponse();
            try
            {
                // 1. Supprimer les liaisons
                var deleteLinksResult = await criteresDal.DeleteStarLevelCriterionByCriterionId(criterionId, ct);
                if (!deleteLinksResult.IsSuccess)
                {
                    response.IsSuccess = false;
                    response.Message = deleteLinksResult.Message;
                    return response;
                }

                // 2. Supprimer le critère
                var deleteCriterionResult = await criteresDal.DeleteCriterion(criterionId, ct);
                if (!deleteCriterionResult.IsSuccess)
                {
                    response.IsSuccess = false;
                    response.Message = deleteCriterionResult.Message;
                    return response;
                }

                response.IsSuccess = true;
                response.Message = $"Critère {criterionId} supprimé avec succès.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Erreur lors de la suppression : {ex.Message}";
            }
            return response;
        }
    }
}
