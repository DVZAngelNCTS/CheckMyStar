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
        Task<SocietiesResponse> GetSocieties(CancellationToken ct);
        Task<SocietyResponse> GetSociety(int identifier, CancellationToken ct);
    }
}
