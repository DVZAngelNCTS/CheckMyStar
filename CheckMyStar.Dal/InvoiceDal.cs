using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal
{
    public class InvoiceDal(ICheckMyStarDbContext dbContext) : IInvoiceDal
    {
        public async Task<InvoiceResult> GetInvoice(int invoiceIdentifier, CancellationToken ct)
        {
            InvoiceResult invoiceResult = new InvoiceResult();

            try
            {
                var invoice = await (from i in dbContext.Invoices.AsNoTracking()
                                    where i.Identifier == invoiceIdentifier
                                    select i).FirstOrDefaultAsync(ct);

                invoiceResult.IsSuccess = true;
                invoiceResult.Invoice = invoice;
            }
            catch (Exception ex)
            {
                invoiceResult.IsSuccess = false;
                invoiceResult.Message = ex.Message;
            }

            return invoiceResult;
        }
    }
}
