using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class GeolocationResponse : BaseResponse
    {
        public List<GeolocationAddressModel>? Addresses { get; set; }
    }
}
