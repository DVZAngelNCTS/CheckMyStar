using CheckMyStar.Data;

namespace CheckMyStar.Dal.Results
{
    public class AssessmentCriteriaResult : BaseResult
    {
        public List<AssessmentCriterionDetail>? AssessmentCriteria { get; set; }
    }
}
