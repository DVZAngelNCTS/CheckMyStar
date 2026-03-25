using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService
{
    public interface ISendMailForService
    {
        Task<BaseResponse> Send(SendMailGetRequest request, CancellationToken ct);
    }
}
