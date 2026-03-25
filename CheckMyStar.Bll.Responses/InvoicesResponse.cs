using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class InvoicesResponse : BaseResponse
    {
        public List<InvoiceModel> Invoices { get; set; } = new List<InvoiceModel>();
    }
}
