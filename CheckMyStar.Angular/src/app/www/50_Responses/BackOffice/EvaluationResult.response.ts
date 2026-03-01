import { BaseResponse } from '../BaseResponse';
import { EvaluationResultModel } from '../../20_Models/BackOffice/EvaluationResult.model';

export interface EvaluationResultResponse extends BaseResponse {
  evaluationResult?: EvaluationResultModel;
}
