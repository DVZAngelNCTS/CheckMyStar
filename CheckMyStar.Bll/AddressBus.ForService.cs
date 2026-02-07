using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll
{
    public partial class AddressBus : IAddressBusForService
    {
        public Task<AddressResponse> GetNextIdentifier(CancellationToken ct)
        {
            return this.GetIdentifier(ct);
        }
    }
}
