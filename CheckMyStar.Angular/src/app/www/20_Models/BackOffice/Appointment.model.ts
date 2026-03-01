import { AddressModel } from '../Common/Address.model';

export interface AppointmentModel {
  identifier?: number;
  appointmentDate?: string;
  address?: AddressModel;
  comment?: string;
}