using CheckMyStar.Data;

namespace CheckMyStar.Dal.Results
{
    public class QuoteLinesResult : BaseResult
    {
        public List<QuoteLine> QuoteLines { get; set; } = new List<QuoteLine>();
    }
}
