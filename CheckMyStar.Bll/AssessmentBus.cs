using AutoMapper;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data;

namespace CheckMyStar.Bll
{
    public partial class AssessmentBus(IUserContextService userContext, IAssessmentDal assessmentDal, IMapper mapper) : IAssessmentBus
    {
        public async Task<AssessmentsResponse> GetAllAssessments(CancellationToken ct)
        {
            AssessmentsResponse assessmentsResponse = new AssessmentsResponse();

            var result = await assessmentDal.GetAssessments(ct);

            if (result.IsSuccess && result.Assessments != null)
            {
                var assessmentModel = mapper.Map<List<AssessmentModel>>(result.Assessments);

                assessmentsResponse.IsSuccess = true;
                assessmentsResponse.Message = result.Message;
                assessmentsResponse.Assessments = assessmentModel;
            }
            else
            {
                assessmentsResponse.IsSuccess = false;
                assessmentsResponse.Message = result.Message;
            }

            return assessmentsResponse;
        }

        public async Task<AssessmentResponse> AddAssessment(AssessmentModel assessmentModel, CancellationToken ct)
        {
            var assessment = mapper.Map<Assessment>(assessmentModel);

            var criteria = assessmentModel.Criteria.Select(c => new AssessmentCriterion
            {
                CriterionId = c.CriterionId,
                Points = c.Points,
                Status = c.Status,
                IsValidated = c.IsValidated,
                Comment = c.Comment
            }).ToList();

            var result = await assessmentDal.AddAssessment(assessment, criteria, ct);

            var response = new AssessmentResponse
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                Assessment = result.Assessment != null ? mapper.Map<AssessmentModel>(result.Assessment) : null
            };

            return response;
        }

        public async Task<BaseResponse> DeleteAssessment(int identifier, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var assessment = await assessmentDal.GetAssessment(identifier, ct);

            if (assessment.IsSuccess)
            {
                if (assessment.Assessment != null)
                {
                    var baseResult = await assessmentDal.DeleteAssessment(assessment.Assessment, ct);

                    if (baseResult.IsSuccess)
                    {
                        result.IsSuccess = true;
                        result.Message = baseResult.Message;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = baseResult.Message;
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Assessment not found.";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "Failed to retrieve the assessment.";
            }

            return result;
        }
    }
}
