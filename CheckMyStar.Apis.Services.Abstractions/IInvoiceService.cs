using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services.Abstractions
{
    public interface IInvoiceService
    {
        Task<InvoiceResponse> GetNextIdentifier(CancellationToken ct);
        Task<InvoicesResponse> GetInvoices(CancellationToken ct);
        Task<InvoicesResponse> GetInvoices(InvoiceGetByInspectorRequest request, CancellationToken ct);
        Task<InvoiceResponse> GetInvoice(InvoiceGetRequest request, CancellationToken ct);
        Task<InvoiceResponse> AddInvoice(InvoiceSaveRequest request, CancellationToken ct);
        Task<BaseResponse> UpdateInvoice(InvoiceSaveRequest request, CancellationToken ct);
        Task<BaseResponse> DeleteInvoice(InvoiceDeleteRequest request, CancellationToken ct);
    }
}
