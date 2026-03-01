using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService
{
    public interface IAppointmentBusForService
    {
        Task<AppointmentResponse> GetNextIdentifier(CancellationToken ct);
        Task<AppointmentResponse> GetAppointmentByFolder(AppointmentGetByFolderRequest request, CancellationToken ct);
        Task<AppointmentResponse> AddAppointment(AppointmentSaveRequest request, CancellationToken ct);
        Task<AppointmentResponse> UpdateAppointment(AppointmentSaveRequest request, CancellationToken ct);
        Task<BaseResponse> DeleteAppointment(AppointmentDeleteRequest request, CancellationToken ct);
    }
}
