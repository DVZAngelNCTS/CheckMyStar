using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services
{
    public class AccommodationService(IAccommodationBusForService accommodationBusForService) : IAccommodationService
    {
        public Task<AccommodationResponse> GetNextIdentifier(CancellationToken ct)
        {
            return accommodationBusForService.GetNextIdentifier(ct);
        }

        public Task<AccommodationsResponse> GetAccommodations(CancellationToken ct)
        {
            return accommodationBusForService.GetAccommodations(ct);
        }

        public Task<BaseResponse> CreateAccommodation(AccommodationSaveRequest request, CancellationToken ct)
        {
            return accommodationBusForService.CreateAccommodation(request, ct);
        }

        public Task<BaseResponse> UpdateAccommodation(AccommodationSaveRequest request, CancellationToken ct)
        {
            return accommodationBusForService.UpdateAccommodation(request, ct);
        }

        public Task<BaseResponse> DeleteAccommodation(int accommodationIdentifier, CancellationToken ct)
        {
            return accommodationBusForService.DeleteAccommodation(accommodationIdentifier, ct);
        }
    }
}
