import { BaseResponse } from '../BaseResponse';

export interface AssessmentResponse extends BaseResponse {
  assessmentId?: number;
  identifier?: number;
}
