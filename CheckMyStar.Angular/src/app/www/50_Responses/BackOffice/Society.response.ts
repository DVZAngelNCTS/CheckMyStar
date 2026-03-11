import { SocietyModel } from '../../20_Models/BackOffice/Society.model';
import { BaseResponse } from '../BaseResponse';

export interface SocietyResponse extends BaseResponse {
  society?: SocietyModel
}