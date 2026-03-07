using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class AddressesResponse : BaseResponse
    {
        public List<AddressModel> Addresses { get; set; } = new();
    }
}
