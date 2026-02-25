using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Requests
{
    public class AssessmentSaveRequest
    {
        public required AssessmentModel Assessment { get; set; }
    }
}
