using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IInvoiceDal
    {
        Task<InvoiceResult> GetNextIdentifier(CancellationToken ct);
        Task<InvoiceResult> GetInvoice(int invoiceIdentifier, CancellationToken ct);
        Task<InvoicesResult> GetInvoices(CancellationToken ct);
        Task<InvoicesResult> GetInvoicesByInspector(int inspectorIdentifier, CancellationToken ct);
        Task<InvoiceResult> AddInvoice(Invoice invoice, CancellationToken ct);
        Task<BaseResult> UpdateInvoice(Invoice invoice, CancellationToken ct);
        Task<BaseResult> DeleteInvoice(Invoice invoice, CancellationToken ct);
    }
}
