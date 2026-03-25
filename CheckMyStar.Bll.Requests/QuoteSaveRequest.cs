using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Requests
{
    public class QuoteSaveRequest
    {
        public QuoteModel Quote { get; set; } = new QuoteModel();
    }
}
