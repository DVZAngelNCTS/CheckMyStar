using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Requests
{
    public class AssessmentResultSaveRequest
    {
        public required AssessmentResultModel AssessmentResult { get; set; }
    }
}
