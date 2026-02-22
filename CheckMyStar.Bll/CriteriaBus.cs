using AutoMapper;
using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data;

namespace CheckMyStar.Bll
{
    public partial class CriteriaBus(ICriteresDal criteresDal, IMapper mapper, IUserContextService userContext, IActivityBus activityBus) : ICriteriaBus
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
                        LastUpdate = g.First().LastUpdate ?? DateTime.UtcNow,
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

        public async Task<BaseResponse> AddCriterion(StarCriterionModel starCriterionModel, StarLevelCriterionModel starLevelCriterionModel, int currentUser, CancellationToken ct)
        {
            var baseResponse = new BaseResponse();

            try
            {
                var dateTime = DateTime.Now;

                var starCriterion = mapper.Map<Criterion>(starCriterionModel);

                var addCriterionResult = await criteresDal.AddCriterion(starCriterion, ct);

                if (!addCriterionResult.IsSuccess)
                {
                    baseResponse.IsSuccess = false;
                    baseResponse.Message = addCriterionResult.Message;

                    await activityBus.AddActivity(addCriterionResult.Message, dateTime, currentUser, addCriterionResult.IsSuccess, ct);

                    return baseResponse;
                }

                await activityBus.AddActivity(addCriterionResult.Message, dateTime, currentUser, addCriterionResult.IsSuccess, ct);

                int newCriterionId = starCriterion.CriterionId;

                var starLevelCriterion = mapper.Map<StarLevelCriterion>(starLevelCriterionModel);

                starLevelCriterion.CriterionId = newCriterionId;

                var addLinkResult = await criteresDal.AddStarLevelCriterion(starLevelCriterion, ct);

                if (!addLinkResult.IsSuccess)
                {
                    baseResponse.IsSuccess = false;
                    baseResponse.Message = addCriterionResult.Message + "<br>" + addLinkResult.Message;

                    await activityBus.AddActivity(addLinkResult.Message, dateTime, currentUser, addLinkResult.IsSuccess, ct);

                    return baseResponse;
                }

                await activityBus.AddActivity(addLinkResult.Message, dateTime, currentUser, addLinkResult.IsSuccess, ct);

                var updateLastUpdateResult = await criteresDal.UpdateStarLevelLastUpdate(starLevelCriterionModel.StarLevelId, ct);

                if (!updateLastUpdateResult.IsSuccess)
                {
                    baseResponse.IsSuccess = false;
                    baseResponse.Message = addCriterionResult.Message + "<br>" + addLinkResult.Message + "<br>" + updateLastUpdateResult.Message;
                    
                    await activityBus.AddActivity(updateLastUpdateResult.Message, dateTime, currentUser, updateLastUpdateResult.IsSuccess, ct);

                    return baseResponse;
                }

                await activityBus.AddActivity(updateLastUpdateResult.Message, dateTime, currentUser, updateLastUpdateResult.IsSuccess, ct);

                baseResponse.IsSuccess = true;
                baseResponse.Message = addCriterionResult.Message + "<br>" + addLinkResult.Message;
            }
            catch (Exception ex)
            {
                baseResponse.IsSuccess = false;
                baseResponse.Message = ex.Message;
            }

            return baseResponse;
        }

        public async Task<BaseResponse> DeleteCriterion(int criterionId, int currentUser, CancellationToken ct)
        {
            var response = new BaseResponse();

            try
            {
                var dateTime = DateTime.Now;

                var starLevelIds = await criteresDal.GetStarLevelIdsByCriterionId(criterionId, ct);

                var deleteLinksResult = await criteresDal.DeleteStarLevelCriterionByCriterionId(criterionId, ct);

                if (!deleteLinksResult.IsSuccess)
                {
                    response.IsSuccess = false;
                    response.Message = deleteLinksResult.Message;

                    await activityBus.AddActivity(deleteLinksResult.Message, dateTime, currentUser, deleteLinksResult.IsSuccess, ct);

                    return response;
                }

                await activityBus.AddActivity(deleteLinksResult.Message, dateTime, currentUser, deleteLinksResult.IsSuccess, ct);

                foreach (var id in starLevelIds)
                {
                    var updateLastUpdateResult = await criteresDal.UpdateStarLevelLastUpdate(id, ct);

                    await activityBus.AddActivity(updateLastUpdateResult.Message, dateTime, currentUser, updateLastUpdateResult.IsSuccess, ct);
                }

                var deleteCriterionResult = await criteresDal.DeleteCriterion(criterionId, ct);

                if (!deleteCriterionResult.IsSuccess)
                {
                    response.IsSuccess = false;
                    response.Message = deleteCriterionResult.Message;

                    await activityBus.AddActivity(deleteCriterionResult.Message, dateTime, currentUser, deleteCriterionResult.IsSuccess, ct);

                    return response;
                }

                await activityBus.AddActivity(deleteCriterionResult.Message, dateTime, currentUser, deleteCriterionResult.IsSuccess, ct);

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

        public async Task<BaseResponse> UpdateCriterion(CriterionUpdateRequest request, int currentUser, CancellationToken ct)
        {
            var response = new BaseResponse();

            try
            {
                var dateTime = DateTime.Now;

                var criterion = mapper.Map<Criterion>(request.Criterion);

                var starLevel = mapper.Map<StarLevel>(request.StarLevel);

                var starLevelCriterion = mapper.Map<StarLevelCriterion>(request.StarLevelCriterion);

                var updateCriterionResult = await criteresDal.UpdateCriterion(criterion, ct);

                if (!updateCriterionResult.IsSuccess)
                {
                    response.IsSuccess = false;
                    response.Message = updateCriterionResult.Message;

                    await activityBus.AddActivity(updateCriterionResult.Message, dateTime, currentUser, updateCriterionResult.IsSuccess, ct);

                    return response;
                }

                await activityBus.AddActivity(updateCriterionResult.Message, dateTime, currentUser, updateCriterionResult.IsSuccess, ct);

                var updateLastUpdateResult = await criteresDal.UpdateStarLevelLastUpdate(starLevel.StarLevelId, ct);

                if (!updateLastUpdateResult.IsSuccess)
                {
                    response.IsSuccess = false;
                    response.Message = updateLastUpdateResult.Message;

                    await activityBus.AddActivity(updateLastUpdateResult.Message, dateTime, currentUser, updateLastUpdateResult.IsSuccess, ct);

                    return response;
                }

                await activityBus.AddActivity(updateLastUpdateResult.Message, dateTime, currentUser, updateLastUpdateResult.IsSuccess, ct);

                var updateTypeResult = await criteresDal.UpdateStarLevelCriterionType(starLevelCriterion.CriterionId, starLevelCriterion.StarLevelId, starLevelCriterion.TypeCode, ct);

                if (!updateTypeResult.IsSuccess)
                {
                    response.IsSuccess = false;
                    response.Message = updateTypeResult.Message;

                    await activityBus.AddActivity(updateTypeResult.Message, dateTime, currentUser, updateTypeResult.IsSuccess, ct);

                    return response;
                }

                await activityBus.AddActivity(updateTypeResult.Message, dateTime, currentUser, updateTypeResult.IsSuccess, ct);

                response.IsSuccess = true;
                response.Message = $"Critère {request.Criterion?.CriterionId} modifié avec succès.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Erreur lors de la modification : {ex.Message}";
            }

            return response;
        }
    }
}
