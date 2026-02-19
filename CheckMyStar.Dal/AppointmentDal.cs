using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal
{
    public class AppointmentDal(ICheckMyStarDbContext dbContext) : IAppointmentDal
    {
        public async Task<AppointmentResult> GetAppointment(int appointmentIdentifier, CancellationToken ct)
        {
            AppointmentResult appointmentResult = new AppointmentResult();

            try
            {
                var appointment = await (from a in dbContext.Appointments.AsNoTracking()
                                        where a.Identifier == appointmentIdentifier
                                        select a).FirstOrDefaultAsync(ct);

                appointmentResult.IsSuccess = true;
                appointmentResult.Appointment = appointment;
            }
            catch (Exception ex)
            {
                appointmentResult.IsSuccess = false;
                appointmentResult.Message = ex.Message;
            }

            return appointmentResult;
        }
    }
}
