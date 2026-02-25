import { BaseResponse } from '../BaseResponse';
import { AssessmentCriterionModel } from '../../20_Models/BackOffice/AssessmentCriterion.model';

export interface AssessmentCriteriaResponse extends BaseResponse {
  assessmentCriteria?: AssessmentCriterionModel[];
}
