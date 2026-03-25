using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IInvoiceBus
    {
        Task<InvoiceResponse> GetIdentifier(CancellationToken ct);
        Task<InvoicesResponse> GetInvoices(CancellationToken ct);
        Task<InvoicesResponse> GetInvoicesByInspector(int inspectorIdentifier, CancellationToken ct);
        Task<InvoiceResponse> GetInvoice(int invoiceIdentifier, CancellationToken ct);
        Task<InvoiceResponse> AddInvoice(InvoiceModel invoiceModel, int currentUser, CancellationToken ct);
        Task<BaseResponse> UpdateInvoice(InvoiceModel invoiceModel, int currentUser, CancellationToken ct);
        Task<BaseResponse> DeleteInvoice(int invoiceIdentifier, int currentUser, CancellationToken ct);
    }
}
