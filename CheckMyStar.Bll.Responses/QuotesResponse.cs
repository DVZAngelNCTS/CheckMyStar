using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class QuotesResponse : BaseResponse
    {
        public List<QuoteModel> Quotes { get; set; } = new List<QuoteModel>();
    }
}
