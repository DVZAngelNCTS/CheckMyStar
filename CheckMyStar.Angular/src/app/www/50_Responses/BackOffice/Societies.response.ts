import { SocietyModel } from '../../20_Models/BackOffice/Society.model';
import { BaseResponse } from '../BaseResponse';

export interface SocietiesResponse extends BaseResponse {
  societies?: SocietyModel[]
}