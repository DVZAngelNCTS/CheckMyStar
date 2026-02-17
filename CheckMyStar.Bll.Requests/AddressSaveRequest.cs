using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Requests
{
    public class AddressSaveRequest
    {
        public required AddressModel Address { get; set; }
    }
}
