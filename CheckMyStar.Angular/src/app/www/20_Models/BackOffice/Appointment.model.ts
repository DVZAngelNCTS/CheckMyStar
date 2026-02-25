import { AddressModel } from '../Common/Address.model';

export interface AppointmentModel {
  identifier?: number | null;
  appointmentDate?: string | null;
  address?: AddressModel | null;
  comment?: string | null;
}
