using CheckMyStar.Dal.Results;
using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal.Abstractions;

public interface ISocietyDal
{
    Task<SocietyResult> AddSociety(Society society, CancellationToken ct);
}