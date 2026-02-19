using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal
{
    public class AssessmentDal(ICheckMyStarDbContext dbContext) : IAssessmentDal
    {
        public async Task<AssessmentsResult> GetAssessments(CancellationToken ct)
        {
            AssessmentsResult result = new AssessmentsResult();

            try
            {
                var assessments = await dbContext.Assessments
                    .Include(a => ((Assessment)a).AssessmentCriteria)
                    .ToListAsync(ct);

                result.Assessments = assessments.Cast<Assessment>().ToList();
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

        public async Task<AssessmentResult> CreateAssessment(Assessment assessment, List<AssessmentCriterion> criteria, CancellationToken ct)
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

                var createdAssessment = await dbContext.Assessments
                    .Include(a => ((Assessment)a).AssessmentCriteria)
                    .FirstOrDefaultAsync(a => a.Identifier == assessment.Identifier, ct);

                result.Assessment = createdAssessment as Assessment;
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

        public async Task<AssessmentResult> DeleteAssessment(int id, CancellationToken ct)
        {
            AssessmentResult result = new AssessmentResult();

            try
            {
                var assessment = await dbContext.Assessments
                    .Include(a => ((Assessment)a).AssessmentCriteria)
                    .FirstOrDefaultAsync(a => a.Identifier == id, ct);

                if (assessment == null)
                {
                    result.IsSuccess = false;
                    result.Message = "Évaluation introuvable";
                    return result;
                }

                var assessmentEntity = assessment as Assessment;
                if (assessmentEntity?.AssessmentCriteria != null && assessmentEntity.AssessmentCriteria.Any())
                {
                    await dbContext.RemoveRangeAsync(assessmentEntity.AssessmentCriteria, ct);
                }

                await dbContext.RemoveAsync(assessment, ct);
                await dbContext.SaveChangesAsync(ct);

                result.IsSuccess = true;
                result.Message = "Évaluation supprimée avec succès";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Erreur lors de la suppression de l'évaluation : {ex.Message}";
            }

            return result;
        }
    }
}
