using AutoMapper;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data;

namespace CheckMyStar.Bll
{
    public partial class InvoiceBus(IUserContextService userContext, IActivityBus activityBus, IInvoiceDal invoiceDal, IMapper mapper) : IInvoiceBus
    {
        public async Task<InvoiceResponse> GetIdentifier(CancellationToken ct)
        {
            var invoiceResult = await invoiceDal.GetNextIdentifier(ct);

            return new InvoiceResponse
            {
                IsSuccess = invoiceResult.IsSuccess,
                Message = invoiceResult.Message,
                Invoice = invoiceResult.Invoice != null ? mapper.Map<InvoiceModel>(invoiceResult.Invoice) : null
            };
        }

        public async Task<InvoicesResponse> GetInvoices(CancellationToken ct)
        {
            var invoicesResult = await invoiceDal.GetInvoices(ct);

            if (!invoicesResult.IsSuccess)
            {
                return new InvoicesResponse
                {
                    IsSuccess = false,
                    Message = invoicesResult.Message
                };
            }

            return new InvoicesResponse
            {
                IsSuccess = true,
                Message = invoicesResult.Message,
                Invoices = mapper.Map<List<InvoiceModel>>(invoicesResult.Invoices)
            };
        }

        public async Task<InvoicesResponse> GetInvoicesByInspector(int inspectorIdentifier, CancellationToken ct)
        {
            var invoicesResult = await invoiceDal.GetInvoicesByInspector(inspectorIdentifier, ct);

            if (!invoicesResult.IsSuccess)
            {
                return new InvoicesResponse
                {
                    IsSuccess = false,
                    Message = invoicesResult.Message
                };
            }

            return new InvoicesResponse
            {
                IsSuccess = true,
                Message = invoicesResult.Message,
                Invoices = mapper.Map<List<InvoiceModel>>(invoicesResult.Invoices)
            };
        }

        public async Task<InvoiceResponse> GetInvoice(int invoiceIdentifier, CancellationToken ct)
        {
            var invoiceResult = await invoiceDal.GetInvoice(invoiceIdentifier, ct);

            if (invoiceResult.IsSuccess && invoiceResult.Invoice != null)
            {
                return new InvoiceResponse
                {
                    IsSuccess = true,
                    Message = string.IsNullOrWhiteSpace(invoiceResult.Message) ? "Facture récupérée avec succès" : invoiceResult.Message,
                    Invoice = mapper.Map<InvoiceModel>(invoiceResult.Invoice)
                };
            }

            return new InvoiceResponse
            {
                IsSuccess = false,
                Message = invoiceResult.IsSuccess ? "Facture non trouvée" : invoiceResult.Message,
                Invoice = null
            };
        }

        public async Task<InvoiceResponse> AddInvoice(InvoiceModel invoiceModel, int currentUser, CancellationToken ct)
        {
            var dateTime = DateTime.Now;

            if (invoiceModel.Identifier == 0)
            {
                var identifierResult = await invoiceDal.GetNextIdentifier(ct);
                if (identifierResult.IsSuccess && identifierResult.Invoice != null)
                {
                    invoiceModel.Identifier = identifierResult.Invoice.Identifier;
                }
                else
                {
                    return new InvoiceResponse
                    {
                        IsSuccess = false,
                        Message = $"Erreur lors de la récupération de l'identifiant : {identifierResult.Message}"
                    };
                }
            }

            invoiceModel.CreatedDate = dateTime;
            invoiceModel.UpdatedDate = null;

            var invoice = mapper.Map<Invoice>(invoiceModel);
            var invoiceResult = await invoiceDal.AddInvoice(invoice, ct);

            await activityBus.AddActivity(invoiceResult.Message, dateTime, currentUser, invoiceResult.IsSuccess, ct);

            return new InvoiceResponse
            {
                IsSuccess = invoiceResult.IsSuccess,
                Message = invoiceResult.Message,
                Invoice = invoiceResult.Invoice != null ? mapper.Map<InvoiceModel>(invoiceResult.Invoice) : null
            };
        }

        public async Task<BaseResponse> UpdateInvoice(InvoiceModel invoiceModel, int currentUser, CancellationToken ct)
        {
            var existingInvoice = await invoiceDal.GetInvoice(invoiceModel.Identifier, ct);

            if (!existingInvoice.IsSuccess)
            {
                return new BaseResponse
                {
                    IsSuccess = false,
                    Message = existingInvoice.Message
                };
            }

            if (existingInvoice.Invoice == null)
            {
                return new BaseResponse
                {
                    IsSuccess = false,
                    Message = "Facture non trouvée"
                };
            }

            var dateTime = DateTime.Now;
            invoiceModel.CreatedDate = existingInvoice.Invoice.CreatedDate;
            invoiceModel.UpdatedDate = dateTime;

            var invoice = mapper.Map<Invoice>(invoiceModel);
            var result = await invoiceDal.UpdateInvoice(invoice, ct);

            await activityBus.AddActivity(result.Message, dateTime, currentUser, result.IsSuccess, ct);

            return new BaseResponse
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message
            };
        }

        public async Task<BaseResponse> DeleteInvoice(int invoiceIdentifier, int currentUser, CancellationToken ct)
        {
            var existingInvoice = await invoiceDal.GetInvoice(invoiceIdentifier, ct);

            if (!existingInvoice.IsSuccess)
            {
                return new BaseResponse
                {
                    IsSuccess = false,
                    Message = existingInvoice.Message
                };
            }

            if (existingInvoice.Invoice == null)
            {
                return new BaseResponse
                {
                    IsSuccess = false,
                    Message = "Facture non trouvée"
                };
            }

            var dateTime = DateTime.Now;
            var result = await invoiceDal.DeleteInvoice(existingInvoice.Invoice, ct);

            await activityBus.AddActivity(result.Message, dateTime, currentUser, result.IsSuccess, ct);

            return new BaseResponse
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message
            };
        }
    }
}
