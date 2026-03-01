using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal
{
    public class AssessmentDal(ICheckMyStarDbContext dbContext) : IAssessmentDal
    {
        public async Task<AssessmentResult> GetAssessment(int identifier, CancellationToken ct)
        {
            AssessmentResult assessmentResult = new AssessmentResult();

            try
            {
                var assessment = await (from a in dbContext.Assessments.AsNoTracking()
                                        where
                                           a.Identifier == identifier
                                        select a).FirstOrDefaultAsync(ct);

                if (assessment != null)
                {
                    assessmentResult.IsSuccess = true;
                    assessmentResult.Assessment = assessment;
                }
                else
                {
                    assessmentResult.IsSuccess = false;
                    assessmentResult.Message = "Évaluation non trouvée";
                }
            }
            catch (Exception ex)
            {
                assessmentResult.IsSuccess = false;
                assessmentResult.Message = $"Erreur lors de la récupération de l'évaluation : {ex.Message}";
            }

            return assessmentResult;
        }

        public async Task<AssessmentsResult> GetAssessments(CancellationToken ct)
        {
            AssessmentsResult result = new AssessmentsResult();

            try
            {
                var assessments = await dbContext.Assessments.ToListAsync();

                result.Assessments = assessments;
                result.IsSuccess = true;
                result.Message = "Évaluations récupérées avec succès";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Erreur lors de la récupération des évaluations : {ex.Message}";
            }

            return result;
        }

        public async Task<AssessmentResult> GetAssessmentByFolder(int folderIdentifier, CancellationToken ct)
        {
            AssessmentResult result = new AssessmentResult();

            try
            {
                var assessment = await (from a in dbContext.Assessments.AsNoTracking()
                                        where a.FolderIdentifier == folderIdentifier
                                        orderby a.CreatedDate descending
                                        select a).FirstOrDefaultAsync(ct);

                if (assessment != null)
                {
                    result.IsSuccess = true;
                    result.Assessment = assessment;
                    result.Message = "Évaluation récupérée avec succès";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Évaluation non trouvée pour ce dossier";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Erreur lors de la récupération de l'évaluation : {ex.Message}";
            }

            return result;
        }

        public async Task<AssessmentCriteriaResult> GetAssessmentCriteria(int assessmentIdentifier, CancellationToken ct)
        {
            AssessmentCriteriaResult result = new AssessmentCriteriaResult();

            try
            {
                var criteriaDetails = await (from ac in dbContext.AssessmentCriteria.AsNoTracking()
                                             join c in dbContext.Criterias.AsNoTracking() on ac.CriterionId equals c.CriterionId
                                             where ac.AssessmentIdentifier == assessmentIdentifier
                                             select new Data.AssessmentCriterionDetail
                                             {
                                                 AssessmentIdentifier = ac.AssessmentIdentifier,
                                                 CriterionId = ac.CriterionId,
                                                 CriterionDescription = c.Description,
                                                 BasePoints = c.BasePoints,
                                                 Points = ac.Points,
                                                 Status = ac.Status,
                                                 IsValidated = ac.IsValidated,
                                                 Comment = ac.Comment
                                             }).ToListAsync(ct);

                result.AssessmentCriteria = criteriaDetails;
                result.IsSuccess = true;
                result.Message = "Critères d'évaluation récupérés avec succès";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Erreur lors de la récupération des critères d'évaluation : {ex.Message}";
            }

            return result;
        }

        public async Task<AssessmentResult> AddAssessment(Data.Assessment assessment, List<Data.AssessmentCriterion> criteria, CancellationToken ct)
        {
            AssessmentResult result = new AssessmentResult();

            try
            {
                assessment.CreatedDate = DateTime.Now;
                assessment.UpdatedDate = null;

                await dbContext.AddAsync(assessment, ct);

                await dbContext.SaveChangesAsync(ct);

                foreach (var criterion in criteria)
                {
                    criterion.AssessmentIdentifier = assessment.Identifier;

                    await dbContext.AddAsync(criterion, ct);
                }

                await dbContext.SaveChangesAsync(ct);

                var createdAssessment = await dbContext.Assessments.FirstOrDefaultAsync(a => a.Identifier == assessment.Identifier, ct);

                result.Assessment = createdAssessment as Data.Assessment;
                result.IsSuccess = true;
                result.Message = "Évaluation créée avec succès";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Erreur lors de la création de l'évaluation : {ex.Message}";
            }

            return result;
        }

        public async Task<AssessmentResult> UpdateAssessment(Data.Assessment assessment, List<Data.AssessmentCriterion> criteria, CancellationToken ct)
        {
            AssessmentResult result = new AssessmentResult();

            try
            {
                assessment.UpdatedDate = DateTime.Now;

                await dbContext.UpdateAsync(assessment, ct);

                var existingCriteria = await dbContext.AssessmentCriteria
                    .Where(ac => ac.AssessmentIdentifier == assessment.Identifier)
                    .ToListAsync(ct);

                foreach (var existingCriterion in existingCriteria)
                {
                    await dbContext.RemoveAsync(existingCriterion, ct);
                }

                await dbContext.SaveChangesAsync(ct);

                foreach (var criterion in criteria)
                {
                    criterion.AssessmentIdentifier = assessment.Identifier;
                    await dbContext.AddAsync(criterion, ct);
                }

                await dbContext.SaveChangesAsync(ct);

                var updatedAssessment = await dbContext.Assessments.FirstOrDefaultAsync(a => a.Identifier == assessment.Identifier, ct);

                result.Assessment = updatedAssessment as Data.Assessment;
                result.IsSuccess = true;
                result.Message = "Évaluation modifiée avec succès";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Erreur lors de la modification de l'évaluation : {ex.Message}";
            }

            return result;
        }

        public async Task<BaseResult> DeleteAssessment(Data.Assessment assessment, CancellationToken ct)
        {
            BaseResult baseResult = new BaseResult();

            try
            {
                await dbContext.RemoveAsync(assessment, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    baseResult.IsSuccess = true;
                    baseResult.Message = $"Assessment {assessment.Identifier} supprimé avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = $"Impossible de supprimer l'assessment {assessment.Identifier}";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = $"Erreur lors de la suppression de l'assessment : {ex.Message}";
            }

            return baseResult;
        }
    }
}
