using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll
{
    public partial class AssessmentBus : IAssessmentBusForService
    {
        public Task<AssessmentsResponse> GetAssessments(CancellationToken ct)
        {
            return this.GetAssessments(ct);
        }

        public Task<AssessmentResponse> GetAssessment(AssessmentGetRequest request, CancellationToken ct)
        {
            return this.GetAssessment(request.Identifier, ct);
        }

        public Task<AssessmentResponse> GetAssessmentByFolder(AssessmentGetByFolderRequest request, CancellationToken ct)
        {
            return this.GetAssessmentByFolder(request.FolderIdentifier, ct);
        }

        public Task<AssessmentCriteriaResponse> GetAssessmentCriteria(AssessmentCriteriaGetRequest request, CancellationToken ct)
        {
            return this.GetAssessmentCriteria(request.AssessmentIdentifier, ct);
        }

        public Task<AssessmentResponse> AddAssessment(AssessmentSaveRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.AddAssessment(request.Assessment, user, ct);
        }

        public Task<AssessmentResponse> UpdateAssessment(AssessmentSaveRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.UpdateAssessment(request.Assessment, user, ct);
        }

        public Task<BaseResponse> DeleteAssessment(AssessmentDeleteRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.DeleteAssessment(request.Identifier, user, ct);
        }
    }
}
