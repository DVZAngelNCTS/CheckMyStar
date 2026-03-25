using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Requests;

namespace CheckMyStar.Apis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController(IInvoiceService invoiceService) : ControllerBase
    {
        [HttpGet("getnextidentifier")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> GetNextIdentifier(CancellationToken ct)
        {
            var identifier = await invoiceService.GetNextIdentifier(ct);

            return Ok(identifier);
        }

        [HttpGet("getinvoices")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> GetInvoices(CancellationToken ct)
        {
            var invoices = await invoiceService.GetInvoices(ct);

            return Ok(invoices);
        }

        [HttpGet("getinvoicesbyinspector")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> GetInvoicesByInspector([FromQuery] InvoiceGetByInspectorRequest request, CancellationToken ct)
        {
            var invoices = await invoiceService.GetInvoices(request, ct);

            return Ok(invoices);
        }

        [HttpGet("getinvoice")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> GetInvoice([FromQuery] InvoiceGetRequest request, CancellationToken ct)
        {
            var invoice = await invoiceService.GetInvoice(request, ct);

            return Ok(invoice);
        }

        [HttpPost("addinvoice")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> AddInvoice([FromBody] InvoiceSaveRequest request, CancellationToken ct)
        {
            var invoice = await invoiceService.AddInvoice(request, ct);

            return Ok(invoice);
        }

        [HttpPut("updateinvoice")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> UpdateInvoice([FromBody] InvoiceSaveRequest request, CancellationToken ct)
        {
            var invoice = await invoiceService.UpdateInvoice(request, ct);

            return Ok(invoice);
        }

        [HttpDelete("deleteinvoice")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> DeleteInvoice([FromBody] InvoiceDeleteRequest request, CancellationToken ct)
        {
            var invoice = await invoiceService.DeleteInvoice(request, ct);

            return Ok(invoice);
        }
    }
}
