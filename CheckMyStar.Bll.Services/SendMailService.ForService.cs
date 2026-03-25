using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Services
{
    public partial class SendMailService : ISendMailForService
    {
        public Task<BaseResponse> Send(SendMailGetRequest request, CancellationToken ct)
        {
            return this.Send(request.To!, request.ResetLink!, ct);
        }
    }
}
