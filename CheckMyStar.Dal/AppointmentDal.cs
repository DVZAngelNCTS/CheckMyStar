using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal
{
    public class AppointmentDal(ICheckMyStarDbContext dbContext) : IAppointmentDal
    {
        public async Task<AppointmentResult> GetNextIdentifier(CancellationToken ct)
        {
            AppointmentResult appointmentResult = new AppointmentResult();

            try
            {
                var existingIdentifiers = await (from a in dbContext.Appointments.AsNoTracking()
                                                 orderby a.Identifier
                                                 select a.Identifier).ToListAsync(ct);

                if (existingIdentifiers.Count == 0)
                {
                    appointmentResult.IsSuccess = true;
                    appointmentResult.Appointment = new Appointment { Identifier = 1 };
                    appointmentResult.Message = "Identifiant récupéré avec succès";
                }
                else
                {
                    int nextIdentifier = 1;

                    foreach (var id in existingIdentifiers)
                    {
                        if (id == nextIdentifier)
                        {
                            nextIdentifier++;
                        }
                        else if (id > nextIdentifier)
                        {
                            break;
                        }
                    }

                    appointmentResult.IsSuccess = true;
                    appointmentResult.Appointment = new Appointment { Identifier = nextIdentifier };
                    appointmentResult.Message = "Identifiant récupéré avec succès";
                }
            }
            catch (Exception ex)
            {
                appointmentResult.IsSuccess = false;
                appointmentResult.Message = ex.Message;
            }

            return appointmentResult;
        }

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

        public async Task<AppointmentResult> AddAppointment(Appointment appointment, CancellationToken ct)
        {
            AppointmentResult appointmentResult = new AppointmentResult();

            try
            {
                await dbContext.AddAsync(appointment, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    appointmentResult.IsSuccess = true;
                    appointmentResult.Appointment = appointment;
                    appointmentResult.Message = $"Rendez-vous {appointment.Identifier} ajouté avec succès";
                }
                else
                {
                    appointmentResult.IsSuccess = false;
                    appointmentResult.Message = $"Impossible d'ajouter le rendez-vous {appointment.Identifier}";
                }
            }
            catch (Exception ex)
            {
                appointmentResult.IsSuccess = false;
                appointmentResult.Message = $"Impossible d'ajouter le rendez-vous {appointment.Identifier} : " + ex.Message;
            }

            return appointmentResult;
        }

        public async Task<AppointmentResult> UpdateAppointment(Appointment appointment, CancellationToken ct)
        {
            AppointmentResult appointmentResult = new AppointmentResult();

            try
            {
                await dbContext.UpdateAsync(appointment, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    appointmentResult.IsSuccess = true;
                    appointmentResult.Appointment = appointment;
                    appointmentResult.Message = $"Rendez-vous {appointment.Identifier} modifié avec succès";
                }
                else
                {
                    appointmentResult.IsSuccess = false;
                    appointmentResult.Message = $"Impossible de modifier le rendez-vous {appointment.Identifier}";
                }
            }
            catch (Exception ex)
            {
                appointmentResult.IsSuccess = false;
                appointmentResult.Message = $"Impossible de modifier le rendez-vous {appointment.Identifier} : " + ex.Message;
            }

            return appointmentResult;
        }

        public async Task<BaseResult> DeleteAppointment(Appointment appointment, CancellationToken ct)
        {
            BaseResult baseResult = new BaseResult();

            try
            {
                await dbContext.RemoveAsync(appointment, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    baseResult.IsSuccess = true;
                    baseResult.Message = $"Rendez-vous {appointment.Identifier} supprimé avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = $"Impossible de supprimer le rendez-vous {appointment.Identifier}";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = $"Impossible de supprimer le rendez-vous {appointment.Identifier} : " + ex.Message;
            }

            return baseResult;
        }
    }
}
