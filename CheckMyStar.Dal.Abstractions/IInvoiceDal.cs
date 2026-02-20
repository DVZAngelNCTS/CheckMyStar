using CheckMyStar.Dal.Results;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IInvoiceDal
    {
        Task<InvoiceResult> GetInvoice(int invoiceIdentifier, CancellationToken ct);
    }
}
