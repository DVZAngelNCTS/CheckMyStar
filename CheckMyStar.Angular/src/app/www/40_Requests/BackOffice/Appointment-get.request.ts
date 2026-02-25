import { AppointmentModel } from "../../20_Models/BackOffice/Appointment.model";

export interface AppointmentGetRequest {
  folderIdentifier?: number | null;
  identifier?: number | null;
}
