using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Requests
{
    public class AccommodationSaveRequest
    {
        public required AccommodationModel Accommodation { get; set; }
    }
}
