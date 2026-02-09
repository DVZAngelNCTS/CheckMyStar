using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class UserEvolutionResponse : BaseResponse
    {
        public List<UserEvolutionModel>? Evolutions { get; set; }
    }
}
