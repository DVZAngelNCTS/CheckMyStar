using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;

namespace CheckMyStar.Bll
{
    public partial class CriteriaBus(ICriteresDal criteresDal) : ICriteriaBus
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

        public async Task<CreateCriterionResponse> CreateCriterionAsync(CreateCriterionRequest request, CancellationToken ct)
        {
            var createCriterionResponse = new CreateCriterionResponse();

            try
            {
                if (string.IsNullOrWhiteSpace(request.Description))
                {
                    createCriterionResponse.IsSuccess = false;
                    createCriterionResponse.Message = "La description du critère est obligatoire.";
                    return createCriterionResponse;
                }

                if (request.BasePoints <= 0)
                {
                    createCriterionResponse.IsSuccess = false;
                    createCriterionResponse.Message = "Les points de base doivent être strictement positifs.";
                    return createCriterionResponse;
                }

                int criterionId = await criteresDal.CreateCriterionAsync(
                    request.Description,
                    request.BasePoints,
                    ct
                );

                foreach (var sl in request.StarLevels)
                {
                    await criteresDal.AddStarLevelCriterionAsync(
                        sl.StarLevelId,
                        criterionId,
                        sl.TypeCode,
                        ct
                    );
                }

                createCriterionResponse.IsSuccess = true;
                createCriterionResponse.CriterionId = criterionId;
                createCriterionResponse.Description = request.Description;
                createCriterionResponse.BasePoints = request.BasePoints;
            }
            catch (Exception ex)
            {
                createCriterionResponse.IsSuccess = false;
                createCriterionResponse.Message = "Erreur lors de la création du critère.";
            }

            return createCriterionResponse;
        }
    }
}
