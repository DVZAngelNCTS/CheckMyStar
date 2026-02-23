using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface ISocietyBus
    {
        Task<SocietyCreateResponse> CreateSociety(SocietyModel societyModel, int currentUser, CancellationToken ct);
        Task<SocietiesResponse> GetSocieties(CancellationToken ct);
    }
}
