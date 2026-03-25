using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IQuoteBus
    {
        Task<QuoteResponse> GetIdentifier(CancellationToken ct);
        Task<QuoteResponse> GetQuote(int quoteIdentifier, CancellationToken ct);
        Task<QuotesResponse> GetQuotes(CancellationToken ct);
        Task<QuotesResponse> GetQuotesByInspector(int inspectorIdentifier, CancellationToken ct);
        Task<BaseResponse> UpdateQuote(QuoteModel quoteModel, int currentUser, CancellationToken ct);
        Task<BaseResponse> DeleteQuote(int identifier, int currentUser, CancellationToken ct);
    }
}
