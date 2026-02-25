using AutoMapper;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data;

namespace CheckMyStar.Bll
{
    public partial class AppointmentBus(IUserContextService userContext, IAppointmentDal appointmentDal, IFolderDal folderDal, IAddressDal addressDal, IMapper mapper, IActivityBus activityBus) : IAppointmentBus
    {
        public async Task<AppointmentResponse> GetNextIdentifier(CancellationToken ct)
        {
            var appointment = await appointmentDal.GetNextIdentifier(ct);

            return mapper.Map<AppointmentResponse>(appointment);
        }

        public async Task<AppointmentResponse> GetAppointmentByFolder(int folderIdentifier, CancellationToken ct)
        {
            AppointmentResponse appointmentResponse = new AppointmentResponse();

            var folder = await folderDal.GetFolder(folderIdentifier, ct);

            if (!folder.IsSuccess || folder.Folder == null)
            {
                appointmentResponse.IsSuccess = false;
                appointmentResponse.Message = "Dossier non trouvé";
                return appointmentResponse;
            }

            if (folder.Folder.AppointmentIdentifier == null)
            {
                appointmentResponse.IsSuccess = false;
                appointmentResponse.Message = "Aucun rendez-vous associé à ce dossier";
                return appointmentResponse;
            }

            var appointment = await appointmentDal.GetAppointment(folder.Folder.AppointmentIdentifier.Value, ct);

            if (appointment.IsSuccess && appointment.Appointment != null)
            {
                var appointmentModel = mapper.Map<AppointmentModel>(appointment.Appointment);

                if (appointment.Appointment.AddressIdentifier.HasValue)
                {
                    var addressResponse = await addressDal.GetAddress(appointment.Appointment.AddressIdentifier.Value, ct);
                    if (addressResponse.IsSuccess && addressResponse.Address != null)
                    {
                        appointmentModel.Address = mapper.Map<AddressModel>(addressResponse.Address);
                    }
                }

                appointmentResponse.IsSuccess = true;
                appointmentResponse.Message = appointment.Message;
                appointmentResponse.Appointment = appointmentModel;
            }
            else
            {
                appointmentResponse.IsSuccess = false;
                appointmentResponse.Message = appointment.Message;
            }

            return appointmentResponse;
        }

        public async Task<AppointmentResponse> AddAppointment(AppointmentModel appointmentModel, int folderIdentifier, int currentUser, CancellationToken ct)
        {
            AppointmentResponse response = new AppointmentResponse();

            var folder = await folderDal.GetFolder(folderIdentifier, ct);

            if (!folder.IsSuccess || folder.Folder == null)
            {
                response.IsSuccess = false;
                response.Message = "Dossier non trouvé";
                await activityBus.AddActivity(response.Message, DateTime.Now, currentUser, false, ct);
                return response;
            }

            var appointment = mapper.Map<Appointment>(appointmentModel);
            appointment.CreatedDate = DateTime.Now;

            var result = await appointmentDal.AddAppointment(appointment, ct);

            if (result.IsSuccess && result.Appointment != null)
            {
                folder.Folder.AppointmentIdentifier = result.Appointment.Identifier;
                await folderDal.UpdateFolder(folder.Folder, ct);

                response.IsSuccess = true;
                response.Message = result.Message;
                response.Appointment = mapper.Map<AppointmentModel>(result.Appointment);
            }
            else
            {
                response.IsSuccess = false;
                response.Message = result.Message;
            }

            await activityBus.AddActivity(result.Message, DateTime.Now, currentUser, result.IsSuccess, ct);

            return response;
        }

        public async Task<AppointmentResponse> UpdateAppointment(AppointmentModel appointmentModel, int currentUser, CancellationToken ct)
        {
            AppointmentResponse response = new AppointmentResponse();

            var existingAppointment = await appointmentDal.GetAppointment(appointmentModel.Identifier, ct);

            if (!existingAppointment.IsSuccess || existingAppointment.Appointment == null)
            {
                response.IsSuccess = false;
                response.Message = "Rendez-vous non trouvé";
                await activityBus.AddActivity(response.Message, DateTime.Now, currentUser, false, ct);
                return response;
            }

            var appointment = mapper.Map<Appointment>(appointmentModel);
            appointment.CreatedDate = existingAppointment.Appointment.CreatedDate;
            appointment.UpdatedDate = DateTime.Now;

            var result = await appointmentDal.UpdateAppointment(appointment, ct);

            response.IsSuccess = result.IsSuccess;
            response.Message = result.Message;
            response.Appointment = result.Appointment != null ? mapper.Map<AppointmentModel>(result.Appointment) : null;

            await activityBus.AddActivity(result.Message, DateTime.Now, currentUser, result.IsSuccess, ct);

            return response;
        }

        public async Task<BaseResponse> DeleteAppointment(int identifier, int folderIdentifier, int currentUser, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var appointment = await appointmentDal.GetAppointment(identifier, ct);

            if (!appointment.IsSuccess || appointment.Appointment == null)
            {
                result.IsSuccess = false;
                result.Message = "Rendez-vous non trouvé";
                await activityBus.AddActivity(result.Message, DateTime.Now, currentUser, false, ct);
                return result;
            }

            var folder = await folderDal.GetFolder(folderIdentifier, ct);

            if (!folder.IsSuccess || folder.Folder == null)
            {
                result.IsSuccess = false;
                result.Message = "Dossier non trouvé";
                await activityBus.AddActivity(result.Message, DateTime.Now, currentUser, false, ct);
                return result;
            }

            folder.Folder.AppointmentIdentifier = null;
            await folderDal.UpdateFolder(folder.Folder, ct);

            var baseResult = await appointmentDal.DeleteAppointment(appointment.Appointment, ct);

            if (baseResult.IsSuccess)
            {
                result.IsSuccess = true;
                result.Message = baseResult.Message;
            }
            else
            {
                result.IsSuccess = false;
                result.Message = baseResult.Message;
            }

            await activityBus.AddActivity(result.Message, DateTime.Now, currentUser, result.IsSuccess, ct);

            return result;
        }
    }
}
