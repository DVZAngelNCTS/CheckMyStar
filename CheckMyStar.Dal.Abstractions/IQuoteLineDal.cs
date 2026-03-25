using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IQuoteLineDal
    {
        Task<QuoteLineResult> GetNextIdentifier(CancellationToken ct);
        Task<QuoteLinesResult> GetQuoteLinesByQuote(int quoteIdentifier, CancellationToken ct);
        Task<BaseResult> AddQuoteLines(List<QuoteLine> quoteLines, CancellationToken ct);
        Task<BaseResult> DeleteQuoteLinesByQuote(int quoteIdentifier, CancellationToken ct);
    }
}
