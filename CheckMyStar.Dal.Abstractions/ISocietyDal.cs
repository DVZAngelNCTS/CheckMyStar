using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions;

public interface ISocietyDal
{
    Task<SocietyResult> AddSociety(Society society, CancellationToken ct);
    Task<BaseResult> DeleteSocity(Society society, CancellationToken ct);
    Task<SocietyResult> GetSociety(int identifier, CancellationToken ct);
    Task<SocietiesResult> GetSocieties(CancellationToken ct);
}