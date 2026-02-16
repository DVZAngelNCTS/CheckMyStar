import { BaseResponse } from '../BaseResponse';

export interface SocietiesResponse extends BaseResponse {
  societies?: {
    identifier: number;
    name: string;
    email: string;
    phone: string;
    addressIdentifier: number | null;
    isActive: boolean;
    createdDate: string;
    updatedDate: string;
  }[];
}