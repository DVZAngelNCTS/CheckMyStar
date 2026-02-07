using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class CountriesResponse : BaseResponse
    {
        public List<CountryModel>? Countries { get; set; }
    }
}
