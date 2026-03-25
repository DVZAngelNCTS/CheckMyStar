using CheckMyStar.Data;

namespace CheckMyStar.Dal.Results
{
    public class QuotesResult : BaseResult
    {
        public List<Quote>? Quotes { get; set; }
    }
}
