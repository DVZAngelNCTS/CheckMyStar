using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class AssessmentResultsResponse : BaseResponse
    {
        public List<AssessmentResultModel>? AssessmentResults { get; set; }
    }
}
