using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll
{
    public partial class InvoiceBus : IInvoiceBusForService
    {
        Task<InvoiceResponse> IInvoiceBusForService.GetNextIdentifier(CancellationToken ct)
        {
            return this.GetIdentifier(ct);
        }

        Task<InvoicesResponse> IInvoiceBusForService.GetInvoices(CancellationToken ct)
        {
            return this.GetInvoices(ct);
        }

        Task<InvoicesResponse> IInvoiceBusForService.GetInvoices(InvoiceGetByInspectorRequest request, CancellationToken ct)
        {
            if (request.InspectorIdentifier.HasValue)
            {
                return this.GetInvoicesByInspector(request.InspectorIdentifier.Value, ct);
            }

            return this.GetInvoices(ct);
        }

        Task<InvoiceResponse> IInvoiceBusForService.GetInvoice(InvoiceGetRequest request, CancellationToken ct)
        {
            return this.GetInvoice(request.Identifier, ct);
        }

        Task<InvoiceResponse> IInvoiceBusForService.AddInvoice(InvoiceSaveRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.AddInvoice(request.Invoice, user, ct);
        }

        Task<BaseResponse> IInvoiceBusForService.UpdateInvoice(InvoiceSaveRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.UpdateInvoice(request.Invoice, user, ct);
        }

        Task<BaseResponse> IInvoiceBusForService.DeleteInvoice(InvoiceDeleteRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.DeleteInvoice(request.Identifier, user, ct);
        }
    }
}
