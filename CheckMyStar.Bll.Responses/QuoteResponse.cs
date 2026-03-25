using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class QuoteResponse : BaseResponse
    {
        public QuoteModel? Quote { get; set; }
    }
}
