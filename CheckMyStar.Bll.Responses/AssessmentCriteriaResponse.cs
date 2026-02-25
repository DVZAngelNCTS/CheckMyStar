using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class AssessmentCriteriaResponse : BaseResponse
    {
        public List<AssessmentCriterionDetailModel>? AssessmentCriteria { get; set; }
    }
}
