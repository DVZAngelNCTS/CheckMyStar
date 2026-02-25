using AutoMapper;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Bll
{
    public partial class AssessmentBus(IUserContextService userContext, IAssessmentDal assessmentDal, IMapper mapper, IActivityBus activityBus) : IAssessmentBus
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

        public async Task<AssessmentResponse> GetAssessment(int identifier, CancellationToken ct)
        {
            AssessmentResponse response = new AssessmentResponse();

            var result = await assessmentDal.GetAssessment(identifier, ct);

            if (result.IsSuccess && result.Assessment != null)
            {
                response.IsSuccess = true;
                response.Message = result.Message;
                response.Assessment = mapper.Map<AssessmentModel>(result.Assessment);
            }
            else
            {
                response.IsSuccess = false;
                response.Message = result.Message;
            }

            return response;
        }

        public async Task<AssessmentResponse> GetAssessmentByFolder(int folderIdentifier, CancellationToken ct)
        {
            AssessmentResponse response = new AssessmentResponse();

            var result = await assessmentDal.GetAssessmentByFolder(folderIdentifier, ct);

            if (result.IsSuccess && result.Assessment != null)
            {
                response.IsSuccess = true;
                response.Message = result.Message;
                response.Assessment = mapper.Map<AssessmentModel>(result.Assessment);
            }
            else
            {
                response.IsSuccess = false;
                response.Message = result.Message;
            }

            return response;
        }

        public async Task<AssessmentCriteriaResponse> GetAssessmentCriteria(int assessmentIdentifier, CancellationToken ct)
        {
            AssessmentCriteriaResponse response = new AssessmentCriteriaResponse();

            var result = await assessmentDal.GetAssessmentCriteria(assessmentIdentifier, ct);

            if (result.IsSuccess)
            {
                response.IsSuccess = true;
                response.Message = result.Message;
                response.AssessmentCriteria = mapper.Map<List<AssessmentCriterionDetailModel>>(result.AssessmentCriteria);
            }
            else
            {
                response.IsSuccess = false;
                response.Message = result.Message;
            }

            return response;
        }

        public async Task<AssessmentResponse> AddAssessment(AssessmentModel assessmentModel, int currentUser, CancellationToken ct)
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

            await activityBus.AddActivity(result.Message, DateTime.Now, currentUser, result.IsSuccess, ct);

            return response;
        }

        public async Task<AssessmentResponse> UpdateAssessment(AssessmentModel assessmentModel, int currentUser, CancellationToken ct)
        {
            AssessmentResponse response = new AssessmentResponse();

            var existingAssessment = await assessmentDal.GetAssessment(assessmentModel.Identifier, ct);

            if (!existingAssessment.IsSuccess || existingAssessment.Assessment == null)
            {
                response.IsSuccess = false;
                response.Message = "Évaluation non trouvée";
                await activityBus.AddActivity(response.Message, DateTime.Now, currentUser, false, ct);
                return response;
            }

            var assessment = mapper.Map<Assessment>(assessmentModel);
            assessment.CreatedDate = existingAssessment.Assessment.CreatedDate;

            var criteria = assessmentModel.Criteria.Select(c => new AssessmentCriterion
            {
                CriterionId = c.CriterionId,
                Points = c.Points,
                Status = c.Status,
                IsValidated = c.IsValidated,
                Comment = c.Comment
            }).ToList();

            var result = await assessmentDal.UpdateAssessment(assessment, criteria, ct);

            response.IsSuccess = result.IsSuccess;
            response.Message = result.Message;
            response.Assessment = result.Assessment != null ? mapper.Map<AssessmentModel>(result.Assessment) : null;

            await activityBus.AddActivity(result.Message, DateTime.Now, currentUser, result.IsSuccess, ct);

            return response;
        }

        public async Task<BaseResponse> DeleteAssessment(int identifier, int currentUser, CancellationToken ct)
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

                    await activityBus.AddActivity(result.Message, DateTime.Now, currentUser, result.IsSuccess, ct);
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
