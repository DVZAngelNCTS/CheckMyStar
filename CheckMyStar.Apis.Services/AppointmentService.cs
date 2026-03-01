using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services
{
    public class AppointmentService(IAppointmentBusForService appointmentBusForService) : IAppointmentService
    {
        public async Task<AppointmentResponse> GetNextIdentifier(CancellationToken ct)
        {
            var appointment = await appointmentBusForService.GetNextIdentifier(ct);

            return appointment;
        }

        public async Task<AppointmentResponse> GetAppointmentByFolder(AppointmentGetByFolderRequest request, CancellationToken ct)
        {
            var appointment = await appointmentBusForService.GetAppointmentByFolder(request, ct);

            return appointment;
        }

        public async Task<AppointmentResponse> AddAppointment(AppointmentSaveRequest request, CancellationToken ct)
        {
            var appointment = await appointmentBusForService.AddAppointment(request, ct);

            return appointment;
        }

        public async Task<AppointmentResponse> UpdateAppointment(AppointmentSaveRequest request, CancellationToken ct)
        {
            var appointment = await appointmentBusForService.UpdateAppointment(request, ct);

            return appointment;
        }

        public async Task<BaseResponse> DeleteAppointment(AppointmentDeleteRequest request, CancellationToken ct)
        {
            var response = await appointmentBusForService.DeleteAppointment(request, ct);

            return response;
        }
    }
}
