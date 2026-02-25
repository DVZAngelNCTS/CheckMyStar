using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal
{
    public class AssessmentResultDal(ICheckMyStarDbContext dbContext) : IAssessmentResultDal
    {
        public async Task<AssessmentResultEntityResult> GetNextIdentifier(CancellationToken ct)
        {
            AssessmentResultEntityResult result = new AssessmentResultEntityResult();

            try
            {
                var existingIdentifiers = await (from ar in dbContext.AssessmentResults.AsNoTracking()
                                                 orderby ar.Identifier
                                                 select ar.Identifier).ToListAsync(ct);

                if (existingIdentifiers.Count == 0)
                {
                    result.IsSuccess = true;
                    result.AssessmentResultEntity = new AssessmentResultEntity { Identifier = 1 };
                    result.Message = "Identifiant récupéré avec succès";
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

                    result.IsSuccess = true;
                    result.AssessmentResultEntity = new AssessmentResultEntity { Identifier = nextIdentifier };
                    result.Message = "Identifiant récupéré avec succès";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Erreur lors de la récupération de l'identifiant : {ex.Message}";
            }

            return result;
        }

        public async Task<AssessmentResultEntityResult> AddAssessmentResult(AssessmentResultEntity assessmentResult, CancellationToken ct)
        {
            AssessmentResultEntityResult result = new AssessmentResultEntityResult();

            try
            {
                if (assessmentResult.Identifier == 0)
                {
                    var nextIdentifierResult = await GetNextIdentifier(ct);
                    if (!nextIdentifierResult.IsSuccess || nextIdentifierResult.AssessmentResultEntity == null)
                    {
                        result.IsSuccess = false;
                        result.Message = "Erreur lors de la récupération de l'identifiant";
                        return result;
                    }
                    assessmentResult.Identifier = nextIdentifierResult.AssessmentResultEntity.Identifier;
                }

                assessmentResult.CreatedDate = DateTime.Now;

                await dbContext.AddAsync(assessmentResult, ct);
                await dbContext.SaveChangesAsync(ct);

                result.AssessmentResultEntity = assessmentResult;
                result.IsSuccess = true;
                result.Message = "Résultat de l'évaluation créé avec succès";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Erreur lors de la création du résultat de l'évaluation : {ex.Message}";
            }

            return result;
        }

        public async Task<AssessmentResultEntityResult> UpdateAssessmentResult(AssessmentResultEntity assessmentResult, CancellationToken ct)
        {
            AssessmentResultEntityResult result = new AssessmentResultEntityResult();

            try
            {
                var existingAssessmentResult = await dbContext.AssessmentResults
                    .FirstOrDefaultAsync(ar => ar.Identifier == assessmentResult.Identifier, ct);

                if (existingAssessmentResult == null)
                {
                    result.IsSuccess = false;
                    result.Message = "Résultat de l'évaluation introuvable";
                    return result;
                }

                existingAssessmentResult.AssesmentIdentifier = assessmentResult.AssesmentIdentifier;
                existingAssessmentResult.IsAccepted = assessmentResult.IsAccepted;
                existingAssessmentResult.MandatoryPointsEarned = assessmentResult.MandatoryPointsEarned;
                existingAssessmentResult.MandatoryThreshold = assessmentResult.MandatoryThreshold;
                existingAssessmentResult.OptionalPointsEarned = assessmentResult.OptionalPointsEarned;
                existingAssessmentResult.OptionalRequired = assessmentResult.OptionalRequired;
                existingAssessmentResult.OncFailedCount = assessmentResult.OncFailedCount;
                existingAssessmentResult.UpdatedDate = DateTime.Now;

                await dbContext.UpdateAsync(existingAssessmentResult, ct);
                await dbContext.SaveChangesAsync(ct);

                result.AssessmentResultEntity = existingAssessmentResult;
                result.IsSuccess = true;
                result.Message = "Résultat de l'évaluation mis à jour avec succès";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Erreur lors de la mise à jour du résultat de l'évaluation : {ex.Message}";
            }

            return result;
        }

        public async Task<AssessmentResultEntitiesResult> GetAssessmentResultsByFolder(int folderIdentifier, CancellationToken ct)
        {
            AssessmentResultEntitiesResult result = new AssessmentResultEntitiesResult();

            try
            {
                var assessmentResults = await (from ar in dbContext.AssessmentResults.AsNoTracking()
                                               join a in dbContext.Assessments.AsNoTracking() on ar.AssesmentIdentifier equals a.Identifier
                                               where a.FolderIdentifier == folderIdentifier
                                               select ar).ToListAsync(ct);

                result.AssessmentResultEntities = assessmentResults;
                result.IsSuccess = true;
                result.Message = "Résultats d'évaluation récupérés avec succès";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Erreur lors de la récupération des résultats d'évaluation : {ex.Message}";
            }

            return result;
        }
    }
}
