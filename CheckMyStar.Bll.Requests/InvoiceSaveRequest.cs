using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Requests
{
    public class InvoiceSaveRequest
    {
        public InvoiceModel Invoice { get; set; } = new InvoiceModel();
    }
}
