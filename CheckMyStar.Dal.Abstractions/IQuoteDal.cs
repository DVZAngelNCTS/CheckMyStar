using CheckMyStar.Dal.Results;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IQuoteDal
    {
        Task<QuoteResult> GetQuote(int quoteIdentifier, CancellationToken ct);
    }
}
