using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions;

public interface ISocietyDal
{
    Task<SocietyResult> GetNextIdentifier(CancellationToken ct);
    Task<BaseResult> AddSociety(Society society, CancellationToken ct);
    Task<BaseResult> UpdateSociety(Society society, CancellationToken ct);
    Task<BaseResult> DeleteSociety(Society society, CancellationToken ct);
    Task<SocietyResult> GetSociety(int identifier, CancellationToken ct);
    Task<SocietiesResult> GetSocieties(CancellationToken ct);
    Task<SocietyResult> GetSociety(string? name, string? email, string? phone, CancellationToken ct);
}