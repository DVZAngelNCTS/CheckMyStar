using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal.Results
{
    public class SocietiesResult : BaseResult
    {
        public List<Society> Societies { get; set; } = new();
    }
}