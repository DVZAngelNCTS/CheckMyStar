using AutoMapper;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data;

namespace CheckMyStar.Bll
{
    public partial class AssessmentResultBus(IUserContextService userContext, IAssessmentResultDal assessmentResultDal, IMapper mapper, IActivityBus activityBus) : IAssessmentResultBus
    {
        public async Task<AssessmentResultResponse> GetNextIdentifier(CancellationToken ct)
        {
            AssessmentResultResponse response = new AssessmentResultResponse();

            var result = await assessmentResultDal.GetNextIdentifier(ct);

            if (result.IsSuccess && result.AssessmentResult != null)
            {
                response.IsSuccess = true;
                response.Message = result.Message;
                response.AssessmentResult = mapper.Map<AssessmentResultModel>(result.AssessmentResult);
            }
            else
            {
                response.IsSuccess = false;
                response.Message = result.Message;
            }

            return response;
        }

        public async Task<AssessmentResultResponse> AddAssessmentResult(AssessmentResultModel assessmentResultModel, int currentUser, CancellationToken ct)
        {
            AssessmentResultResponse response = new AssessmentResultResponse();

            var assessmentResult = mapper.Map<AssessmentResult>(assessmentResultModel);

            var result = await assessmentResultDal.AddAssessmentResult(assessmentResult, ct);

            response.IsSuccess = result.IsSuccess;
            response.Message = result.Message;
            response.AssessmentResult = result.AssessmentResult != null ? mapper.Map<AssessmentResultModel>(result.AssessmentResult) : null;

            await activityBus.AddActivity(result.Message, DateTime.Now, currentUser, result.IsSuccess, ct);

            return response;
        }

        public async Task<AssessmentResultResponse> UpdateAssessmentResult(AssessmentResultModel assessmentResultModel, int currentUser, CancellationToken ct)
        {
            AssessmentResultResponse response = new AssessmentResultResponse();

            var assessmentResult = mapper.Map<AssessmentResult>(assessmentResultModel);

            var result = await assessmentResultDal.UpdateAssessmentResult(assessmentResult, ct);

            response.IsSuccess = result.IsSuccess;
            response.Message = result.Message;
            response.AssessmentResult = result.AssessmentResult != null ? mapper.Map<AssessmentResultModel>(result.AssessmentResult) : null;

            await activityBus.AddActivity(result.Message, DateTime.Now, currentUser, result.IsSuccess, ct);

            return response;
        }

        public async Task<AssessmentResultsResponse> GetAssessmentResultsByFolder(int folderIdentifier, CancellationToken ct)
        {
            AssessmentResultsResponse response = new AssessmentResultsResponse();

            var result = await assessmentResultDal.GetAssessmentResultsByFolder(folderIdentifier, ct);

            if (result.IsSuccess && result.AssessmentResultEntities != null)
            {
                response.IsSuccess = true;
                response.Message = result.Message;
                response.AssessmentResults = mapper.Map<List<AssessmentResultModel>>(result.AssessmentResultEntities);
            }
            else
            {
                response.IsSuccess = false;
                response.Message = result.Message;
            }

            return response;
        }
    }
}
