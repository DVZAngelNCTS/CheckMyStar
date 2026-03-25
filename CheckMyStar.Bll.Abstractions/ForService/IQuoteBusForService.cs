using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService
{
    public interface IQuoteBusForService
    {
        Task<QuoteResponse> GetNextIdentifier(CancellationToken ct);
        Task<QuoteResponse> GetQuote(int quoteIdentifier, CancellationToken ct);
        Task<QuotesResponse> GetQuotes(CancellationToken ct);
        Task<QuotesResponse> GetQuotes(QuoteGetRequest request, CancellationToken ct);
        Task<QuoteResponse> AddQuote(QuoteSaveRequest request, CancellationToken ct);
        Task<BaseResponse> UpdateQuote(QuoteSaveRequest request, CancellationToken ct);
        Task<BaseResponse> DeleteQuote(QuoteDeleteRequest request, CancellationToken ct);
    }
}
