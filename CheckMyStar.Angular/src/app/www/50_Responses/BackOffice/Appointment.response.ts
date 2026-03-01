import { BaseResponse } from '../BaseResponse';
import { AppointmentModel } from '../../20_Models/BackOffice/Appointment.model';

export interface AppointmentResponse extends BaseResponse {
  appointment?: AppointmentModel;
  identifier?: number;
}
