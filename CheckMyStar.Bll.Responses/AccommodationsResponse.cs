using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class AccommodationsResponse : BaseResponse
    {
        public List<AccommodationModel> Accommodations { get; set; } = new List<AccommodationModel>();
    }
}
