using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll
{
    public partial class SocietyBus : ISocietyBusForService
    {
        public Task<SocietyResponse> AddSociety(SocietySaveRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.AddSociety(request.Society!, user, ct);
        }

        public Task<SocietiesResponse> GetSocieties(CancellationToken ct)
        {
            return this.GetAllSocieties(ct);
        }
    }
}
