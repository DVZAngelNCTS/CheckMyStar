import { AppointmentModel } from "../../20_Models/BackOffice/Appointment.model";

export interface AppointmentSaveRequest {
  appointment: AppointmentModel | null;
  folderIdentifier: number;
}
