using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll
{
    public partial class SocietyBus : ISocietyBusForService
    {
        public Task<SocietyResponse> GetNextIdentifier(CancellationToken ct)
        {
            return this.GetIdentifier(ct);
        }

        public Task<BaseResponse> AddSociety(SocietySaveRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.AddSociety(request.Society!, user, ct);
        }

        public Task<BaseResponse> UpdateSociety(SocietySaveRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.UpdateSociety(request.Society!, user, ct);
        }

        public Task<BaseResponse> DeleteSociety(SocietyDeleteRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.DeleteSociety(request.Identifier, user, ct);
        }

        public Task<SocietiesResponse> GetSocieties(CancellationToken ct)
        {
            return this.GetAllSocieties(ct);
        }

        public Task<SocietyResponse> GetSociety(SocietyGetRequest request, CancellationToken ct)
        {
            return this.GetSociety(request.Identifier, ct);
        }
    }
}
