using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IAppointmentDal
    {
        Task<AppointmentResult> GetNextIdentifier(CancellationToken ct);
        Task<AppointmentResult> GetAppointment(int appointmentIdentifier, CancellationToken ct);
        Task<AppointmentResult> AddAppointment(Appointment appointment, CancellationToken ct);
        Task<AppointmentResult> UpdateAppointment(Appointment appointment, CancellationToken ct);
        Task<BaseResult> DeleteAppointment(Appointment appointment, CancellationToken ct);
    }
}
