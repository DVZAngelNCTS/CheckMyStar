using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class AssessmentResponse : BaseResponse
    {
        public AssessmentModel? Assessment { get; set; }
    }
}
