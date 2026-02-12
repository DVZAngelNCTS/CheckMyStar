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

        public async Task<UpdateCriterionResponse> UpdateCriterionAsync(UpdateCriterionRequest request, CancellationToken ct)
        {
            var updateCriterionResponse = new UpdateCriterionResponse();

            try
            {
                // Vérifier que le critère existe
                var criteriaDetails = await criteresDal.GetStarCriteriaDetails(ct);
                var existingCriterion = criteriaDetails.StarCriteriaDetails?
                    .FirstOrDefault(c => c.CriterionId == request.CriterionId);

                if (existingCriterion == null)
                {
                    updateCriterionResponse.IsSuccess = false;
                    updateCriterionResponse.Message = "Le critère n'existe pas.";
                    return updateCriterionResponse;
                }

                if (string.IsNullOrWhiteSpace(request.Description))
                {
                    updateCriterionResponse.IsSuccess = false;
                    updateCriterionResponse.Message = "La description du critère est obligatoire.";
                    return updateCriterionResponse;
                }

                if (request.BasePoints <= 0)
                {
                    updateCriterionResponse.IsSuccess = false;
                    updateCriterionResponse.Message = "Les points de base doivent être strictement positifs.";
                    return updateCriterionResponse;
                }

                // Mettre à jour le critère
                var updated = await criteresDal.UpdateCriterionAsync(
                    request.CriterionId,
                    request.Description,
                    request.BasePoints,
                    ct
                );

                if (!updated)
                {
                    updateCriterionResponse.IsSuccess = false;
                    updateCriterionResponse.Message = "Échec de la mise à jour du critère.";
                    return updateCriterionResponse;
                }

                // Supprimer les anciennes associations
                await criteresDal.DeleteStarLevelCriteriaAsync(request.CriterionId, ct);

                // Ajouter les nouvelles associations
                foreach (var sl in request.StarLevels)
                {
                    await criteresDal.AddStarLevelCriterionAsync(
                        sl.StarLevelId,
                        request.CriterionId,
                        sl.TypeCode,
                        ct
                    );
                }

                updateCriterionResponse.IsSuccess = true;
                updateCriterionResponse.Message = "Critère mis à jour avec succès.";
                updateCriterionResponse.CriterionId = request.CriterionId;
                updateCriterionResponse.Description = request.Description;
                updateCriterionResponse.BasePoints = request.BasePoints;
            }
            catch (Exception ex)
            {
                updateCriterionResponse.IsSuccess = false;
                updateCriterionResponse.Message = "Erreur lors de la mise à jour du critère.";
            }

            return updateCriterionResponse;
        }

    }
}
