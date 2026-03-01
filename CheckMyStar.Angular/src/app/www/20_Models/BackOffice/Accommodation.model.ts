import { AccommodationTypeModel } from './AccommodationType.model'
import { UserModel } from '../Common/User.model';
import { AddressModel } from '../Common/Address.model';
import { FolderStatusModel } from './FolderStatus.model';

export interface AccommodationModel {
  identifier: number;
  accommodationName?: string;
  accommodationPhone?: string ;
  accommodationType?: AccommodationTypeModel;
  accommodationCurrentStar?: number;
  address?: AddressModel;
  isActive: boolean;
}