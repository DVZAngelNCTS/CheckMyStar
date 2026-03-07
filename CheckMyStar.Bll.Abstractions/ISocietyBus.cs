using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface ISocietyBus
    {
        Task<SocietyResponse> GetIdentifier(CancellationToken ct);
        Task<BaseResponse> AddSociety(SocietyModel societyModel, int currentUser, CancellationToken ct);
        Task<BaseResponse> UpdateSociety(SocietyModel societyModel, int currentUser, CancellationToken ct);
        Task<BaseResponse> DeleteSociety(int identifier, int currentUser, CancellationToken ct);
        Task<BaseResponse> EnabledSociety(int identifier, bool isActive, int currentUser, CancellationToken ct);
        Task<SocietiesResponse> GetAllSocieties(string? name, string? email, string? phone, string? address, CancellationToken ct);
        Task<SocietyResponse> GetSociety(int identifier, CancellationToken ct);
    }
}
