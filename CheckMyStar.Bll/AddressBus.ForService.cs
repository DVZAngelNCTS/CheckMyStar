using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll
{
    public partial class AddressBus : IAddressBusForService
    {
        public Task<AddressResponse> GetNextIdentifier(CancellationToken ct)
        {
            return this.GetIdentifier(ct);
        }

        public Task<BaseResponse> AddAddress(AddressSaveRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.AddAddress(request.Address, user, ct);
        }

        public Task<BaseResponse> UpdateAddress(AddressSaveRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.UpdateAddress(request.Address, user, ct);
        }
    }
}
