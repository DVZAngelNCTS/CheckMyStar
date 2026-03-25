using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal
{
    public class InvoiceDal(ICheckMyStarDbContext dbContext) : IInvoiceDal
    {
        public async Task<InvoiceResult> GetNextIdentifier(CancellationToken ct)
        {
            InvoiceResult invoiceResult = new InvoiceResult();

            try
            {
                var existingIdentifiers = await (from i in dbContext.Invoices.AsNoTracking()
                                                 orderby i.Identifier
                                                 select i.Identifier).ToListAsync(ct);

                int nextIdentifier;

                if (existingIdentifiers.Count == 0)
                {
                    nextIdentifier = 1;
                }
                else
                {
                    nextIdentifier = 1;

                    foreach (var id in existingIdentifiers)
                    {
                        if (id == nextIdentifier)
                        {
                            nextIdentifier++;
                        }
                        else if (id > nextIdentifier)
                        {
                            break;
                        }
                    }
                }

                invoiceResult.IsSuccess = true;
                invoiceResult.Invoice = new Invoice
                {
                    Identifier = nextIdentifier
                };
                invoiceResult.Message = "Identifiant récupéré avec succès";
            }
            catch (Exception ex)
            {
                invoiceResult.IsSuccess = false;
                invoiceResult.Message = ex.Message;
            }

            return invoiceResult;
        }

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

        public async Task<InvoicesResult> GetInvoices(CancellationToken ct)
        {
            InvoicesResult invoicesResult = new InvoicesResult();

            try
            {
                var invoices = await (from i in dbContext.Invoices.AsNoTracking()
                                      select i).ToListAsync(ct);

                invoicesResult.IsSuccess = true;
                invoicesResult.Invoices = invoices;
                invoicesResult.Message = "Factures récupérées avec succès";
            }
            catch (Exception ex)
            {
                invoicesResult.IsSuccess = false;
                invoicesResult.Message = ex.Message;
            }

            return invoicesResult;
        }

        public async Task<InvoicesResult> GetInvoicesByInspector(int inspectorIdentifier, CancellationToken ct)
        {
            InvoicesResult invoicesResult = new InvoicesResult();

            try
            {
                var invoices = await (from i in dbContext.Invoices.AsNoTracking()
                                      join q in dbContext.Quotes.AsNoTracking() on i.QuoteIdentifier equals q.Identifier
                                      where q.InspectorIdentifier == inspectorIdentifier
                                      select i).ToListAsync(ct);

                invoicesResult.IsSuccess = true;
                invoicesResult.Invoices = invoices;
                invoicesResult.Message = "Factures récupérées avec succès";
            }
            catch (Exception ex)
            {
                invoicesResult.IsSuccess = false;
                invoicesResult.Message = ex.Message;
            }

            return invoicesResult;
        }

        public async Task<InvoiceResult> AddInvoice(Invoice invoice, CancellationToken ct)
        {
            InvoiceResult invoiceResult = new InvoiceResult();

            try
            {
                await dbContext.AddAsync(invoice, ct);

                bool result = await dbContext.SaveChangesAsync(ct) > 0;

                if (result)
                {
                    invoiceResult.IsSuccess = true;
                    invoiceResult.Invoice = invoice;
                    invoiceResult.Message = $"Facture {invoice.Identifier} ajoutée avec succès";
                }
                else
                {
                    invoiceResult.IsSuccess = false;
                    invoiceResult.Message = $"Impossible d'ajouter la facture {invoice.Identifier}";
                }
            }
            catch (Exception ex)
            {
                invoiceResult.IsSuccess = false;
                invoiceResult.Message = $"Impossible d'ajouter la facture {invoice.Identifier} : {ex.Message}";
            }

            return invoiceResult;
        }

        public async Task<BaseResult> UpdateInvoice(Invoice invoice, CancellationToken ct)
        {
            BaseResult result = new BaseResult();

            try
            {
                invoice.UpdatedDate = DateTime.Now;
                await dbContext.UpdateAsync(invoice, ct);
                await dbContext.SaveChangesAsync(ct);

                result.IsSuccess = true;
                result.Message = "Facture modifiée avec succès";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<BaseResult> DeleteInvoice(Invoice invoice, CancellationToken ct)
        {
            BaseResult result = new BaseResult();

            try
            {
                await dbContext.RemoveAsync(invoice, ct);
                await dbContext.SaveChangesAsync(ct);

                result.IsSuccess = true;
                result.Message = "Facture supprimée avec succès";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
