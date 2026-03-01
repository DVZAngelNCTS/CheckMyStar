using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IAppointmentBus
    {
        Task<AppointmentResponse> GetNextIdentifier(CancellationToken ct);
        Task<AppointmentResponse> GetAppointmentByFolder(int folderIdentifier, CancellationToken ct);
        Task<AppointmentResponse> AddAppointment(AppointmentModel appointmentModel, int folderIdentifier, int currentUser, CancellationToken ct);
        Task<AppointmentResponse> UpdateAppointment(AppointmentModel appointmentModel, int currentUser, CancellationToken ct);
        Task<BaseResponse> DeleteAppointment(int identifier, int folderIdentifier, int currentUser, CancellationToken ct);
    }
}
