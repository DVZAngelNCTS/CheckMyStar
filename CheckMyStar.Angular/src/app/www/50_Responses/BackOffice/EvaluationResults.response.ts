import { BaseResponse } from '../BaseResponse';
import { EvaluationResultModel } from '../../20_Models/BackOffice/EvaluationResult.model';

export interface EvaluationResultsResponse extends BaseResponse {
  assessmentResults?: EvaluationResultModel[];
}
