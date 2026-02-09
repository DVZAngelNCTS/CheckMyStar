using CheckMyStar.Dal.Models;

namespace CheckMyStar.Dal.Results
{
    public class UserEvolutionResult : BaseResult
    {
        public List<UserEvolution>? Evolutions { get; set; }
    }
}
