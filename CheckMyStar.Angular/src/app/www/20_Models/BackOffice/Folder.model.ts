import { CountryModel } from '../Common/Country.model';

export interface FolderModel {
  identifier: number;
  accommodationType?: AccommodationTypeModel | null;
  accommodation?: AccommodationModel | null;
  ownerUser?: FolderUserModel | null;
  inspectorUser?: FolderUserModel | null;
  folderStatus?: unknown | null;
  quote?: unknown | null;
  invoice?: unknown | null;
  appointment?: unknown | null;
  createdDate: string;
  updatedDate: string;
  accommodationTypeIdentifier?: number;
  accommodationIdentifier?: number;
  ownerUserIdentifier?: number;
  inspectorUserIdentifier?: number;
  folderStatusIdentifier?: number;
  quoteIdentifier?: number | null;
  invoiceIdentifier?: number | null;
  appointmentIdentifier?: number | null;
}

export interface AccommodationTypeModel {
  identifier: number;
  name?: string | null;
}

export interface AccommodationModel {
  identifier: number;
  accommodationName: string;
  accommodationPhone?: string | null;
  accommodationType?: AccommodationTypeModel | null;
  accommodationCurrentStar?: number | null;
  address?: FolderAddressModel | null;
  isActive: boolean;
  createdDate?: string | null;
  updatedDate?: string | null;
}

export interface FolderUserModel {
  identifier: number;
  civility: number;
  lastName: string;
  firstName: string;
  societyIdentifier?: number | null;
  email: string;
  phone?: string | null;
  password: string;
  role: number;
  address?: FolderAddressModel | null;
  isActive: boolean;
  isFirstConnection: boolean;
  createdDate?: string | null;
  updatedDate?: string | null;
}

export interface FolderAddressModel {
  identifier: number;
  number?: string | null;
  addressLine?: string | null;
  city?: string | null;
  zipCode?: string | null;
  region?: string | null;
  country?: CountryModel | null;
  createdDate?: string | null;
  updatedDate?: string | null;
}
