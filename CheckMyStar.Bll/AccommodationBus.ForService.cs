using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll
{
    public partial class AccommodationBus : IAccommodationBusForService
    {
        Task<AccommodationResponse> IAccommodationBusForService.GetNextIdentifier(CancellationToken ct)
        {
            return this.GetIdentifier(ct);
        }

        Task<AccommodationsResponse> IAccommodationBusForService.GetAccommodations(CancellationToken ct)
        {
            return this.GetAccommodations(ct);
        }

        public Task<BaseResponse> CreateAccommodation(AccommodationSaveRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.AddAccommodation(request.Accommodation, user, ct);
        }

        public Task<BaseResponse> DeleteAccommodation(int accommodationIdentifier, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.DeleteAccommodation(accommodationIdentifier, user, ct);
        }
    }
}
