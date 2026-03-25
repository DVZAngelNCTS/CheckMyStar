using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll
{
    public partial class QuoteBus : IQuoteBusForService
    {
        Task<QuoteResponse> IQuoteBusForService.GetNextIdentifier(CancellationToken ct)
        {
            return this.GetIdentifier(ct);
        }

        Task<QuoteResponse> IQuoteBusForService.GetQuote(int quoteIdentifier, CancellationToken ct)
        {
            return this.GetQuote(quoteIdentifier, ct);
        }

        Task<QuotesResponse> IQuoteBusForService.GetQuotes(CancellationToken ct)
        {
            return this.GetQuotes(ct);
        }

        Task<QuotesResponse> IQuoteBusForService.GetQuotes(QuoteGetRequest request, CancellationToken ct)
        {
            if (request.InspectorIdentifier.HasValue)
            {
                return this.GetQuotesByInspector(request.InspectorIdentifier.Value, ct);
            }

            return this.GetQuotes(ct);
        }

        Task<QuoteResponse> IQuoteBusForService.AddQuote(QuoteSaveRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.AddQuote(request.Quote, user, ct);
        }

        Task<BaseResponse> IQuoteBusForService.UpdateQuote(QuoteSaveRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.UpdateQuote(request.Quote, user, ct);
        }

        Task<BaseResponse> IQuoteBusForService.DeleteQuote(QuoteDeleteRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.DeleteQuote(request.Identifier, user, ct);
        }
    }
}
