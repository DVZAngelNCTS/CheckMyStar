using CheckMyStar.Data;

namespace CheckMyStar.Dal.Results
{
    public class InvoicesResult : BaseResult
    {
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
