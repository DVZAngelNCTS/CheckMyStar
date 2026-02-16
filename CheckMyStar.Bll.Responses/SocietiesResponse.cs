using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class SocietiesResponse : BaseResponse
    {
        public List<SocietyModel> Societies { get; set; } = new();
    }
}