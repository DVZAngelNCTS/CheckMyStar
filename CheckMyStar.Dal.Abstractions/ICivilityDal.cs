using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface ICivilityDal
    {
        Task<CivilityResult> GetCivilities(CancellationToken ct);
    }
}
