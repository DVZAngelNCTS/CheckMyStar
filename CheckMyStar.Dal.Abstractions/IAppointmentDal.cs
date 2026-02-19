using CheckMyStar.Dal.Results;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IAppointmentDal
    {
        Task<AppointmentResult> GetAppointment(int appointmentIdentifier, CancellationToken ct);
    }
}
