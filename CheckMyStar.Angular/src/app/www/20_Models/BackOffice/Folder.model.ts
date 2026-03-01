import { AccommodationModel} from './Accommodation.model';
import { UserModel } from '../Common/User.model';
import { FolderStatusModel } from './FolderStatus.model';
import { QuoteModel} from './Quote.model';
import { InvoiceModel } from './Invoice.model';
import { AppointmentModel } from './Appointment.model';

export interface FolderModel {
  identifier: number;
  accommodation?: AccommodationModel;
  owner?: UserModel;
  inspector?: UserModel;
  folderStatus?: FolderStatusModel;
  quote?: QuoteModel;
  invoice?: InvoiceModel;
  appointment?: AppointmentModel;
  isActive: boolean;
  updatedDate: Date;
}

