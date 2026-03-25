using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services
{
    public class InvoiceService(IInvoiceBusForService invoiceBusForService) : IInvoiceService
    {
        public async Task<InvoiceResponse> GetNextIdentifier(CancellationToken ct)
        {
            var invoice = await invoiceBusForService.GetNextIdentifier(ct);

            return invoice;
        }

        public async Task<InvoicesResponse> GetInvoices(CancellationToken ct)
        {
            var invoices = await invoiceBusForService.GetInvoices(ct);

            return invoices;
        }

        public async Task<InvoicesResponse> GetInvoices(InvoiceGetByInspectorRequest request, CancellationToken ct)
        {
            var invoices = await invoiceBusForService.GetInvoices(request, ct);

            return invoices;
        }

        public async Task<InvoiceResponse> GetInvoice(InvoiceGetRequest request, CancellationToken ct)
        {
            var invoice = await invoiceBusForService.GetInvoice(request, ct);

            return invoice;
        }

        public async Task<InvoiceResponse> AddInvoice(InvoiceSaveRequest request, CancellationToken ct)
        {
            var invoice = await invoiceBusForService.AddInvoice(request, ct);

            return invoice;
        }

        public async Task<BaseResponse> UpdateInvoice(InvoiceSaveRequest request, CancellationToken ct)
        {
            var result = await invoiceBusForService.UpdateInvoice(request, ct);

            return result;
        }

        public async Task<BaseResponse> DeleteInvoice(InvoiceDeleteRequest request, CancellationToken ct)
        {
            var result = await invoiceBusForService.DeleteInvoice(request, ct);

            return result;
        }
    }
}
