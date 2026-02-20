using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class AssessmentsResponse : BaseResponse
    {
        public List<AssessmentModel>? Assessments { get; set; }
    }
}
