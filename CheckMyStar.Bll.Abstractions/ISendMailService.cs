using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface ISendMailService
    {
        Task<BaseResponse> Send(SendMailGetRequest request, CancellationToken ct);
    }
}
