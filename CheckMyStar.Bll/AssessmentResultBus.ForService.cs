using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll
{
    public partial class AssessmentResultBus : IAssessmentResultBusForService
    {
        Task<AssessmentResultResponse> IAssessmentResultBusForService.GetNextIdentifier(CancellationToken ct)
        {
            return this.GetNextIdentifier(ct);
        }

        Task<AssessmentResultResponse> IAssessmentResultBusForService.AddAssessmentResult(AssessmentResultSaveRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.AddAssessmentResult(request.AssessmentResult, user, ct);
        }

        Task<AssessmentResultResponse> IAssessmentResultBusForService.UpdateAssessmentResult(AssessmentResultSaveRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.UpdateAssessmentResult(request.AssessmentResult, user, ct);
        }

        Task<AssessmentResultsResponse> IAssessmentResultBusForService.GetAssessmentResultsByFolder(AssessmentResultGetByFolderRequest request, CancellationToken ct)
        {
            return this.GetAssessmentResultsByFolder(request.FolderIdentifier, ct);
        }
    }
}
