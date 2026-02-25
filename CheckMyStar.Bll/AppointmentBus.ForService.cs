using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll
{
    public partial class AppointmentBus : IAppointmentBusForService
    {
        Task<AppointmentResponse> IAppointmentBusForService.GetNextIdentifier(CancellationToken ct)
        {
            return this.GetNextIdentifier(ct);
        }

        Task<AppointmentResponse> IAppointmentBusForService.GetAppointmentByFolder(AppointmentGetByFolderRequest request, CancellationToken ct)
        {
            return this.GetAppointmentByFolder(request.FolderIdentifier, ct);
        }

        Task<AppointmentResponse> IAppointmentBusForService.AddAppointment(AppointmentSaveRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.AddAppointment(request.Appointment, request.FolderIdentifier, user, ct);
        }

        Task<AppointmentResponse> IAppointmentBusForService.UpdateAppointment(AppointmentSaveRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.UpdateAppointment(request.Appointment, user, ct);
        }

        Task<BaseResponse> IAppointmentBusForService.DeleteAppointment(AppointmentDeleteRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.DeleteAppointment(request.Identifier, request.FolderIdentifier, user, ct);
        }
    }
}
