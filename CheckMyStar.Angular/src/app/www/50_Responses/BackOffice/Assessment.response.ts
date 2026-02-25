import { BaseResponse } from '../BaseResponse';
import { AssessmentModel } from '../../20_Models/BackOffice/Assessment.model';

export interface AssessmentResponse extends BaseResponse {
  assessment?: AssessmentModel;
}
