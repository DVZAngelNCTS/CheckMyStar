using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class InvoiceResponse : BaseResponse
    {
        public InvoiceModel? Invoice { get; set; }
    }
}
