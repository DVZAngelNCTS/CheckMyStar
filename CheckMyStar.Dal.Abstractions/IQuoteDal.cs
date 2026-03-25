using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IQuoteDal
    {
        Task<QuoteResult> GetNextIdentifier(CancellationToken ct);
        Task<QuoteResult> GetQuote(int quoteIdentifier, CancellationToken ct);
        Task<QuotesResult> GetQuotes(CancellationToken ct);
        Task<QuotesResult> GetQuotesByInspector(int inspectorIdentifier, CancellationToken ct);
        Task<QuoteResult> AddQuote(Quote quote, CancellationToken ct);
        Task<BaseResult> UpdateQuote(Quote quote, CancellationToken ct);
        Task<BaseResult> DeleteQuote(Quote quote, CancellationToken ct);
    }
}
