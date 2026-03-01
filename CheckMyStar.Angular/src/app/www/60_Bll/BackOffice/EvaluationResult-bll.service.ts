import { Injectable } from '@angular/core';
import { switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { EvaluationResultDalService } from '../../70_Dal/BackOffice/EvaluationResult-dal.service';
import { EvaluationResultResponse } from '../../50_Responses/BackOffice/EvaluationResult.response';
import { EvaluationResultsResponse } from '../../50_Responses/BackOffice/EvaluationResults.response';

export interface EvaluationResultPayload {
  assessmentIdentifier: number;
  isAccepted: boolean;
  mandatoryPointsEarned: number;
  mandatoryThreshold: number;
  optionalPointsEarned: number;
  optionalRequired: number;
  onceFailedCount: number;
}

@Injectable({
  providedIn: 'root'
})
export class EvaluationResultBllService {
  constructor(private evaluationResultDal: EvaluationResultDalService) {}

  getAssessmentResultsByFolder$(folderIdentifier: number): Observable<EvaluationResultsResponse> {
    return this.evaluationResultDal.getAssessmentResultsByFolder$(folderIdentifier);
  }

  saveEvaluationResult$(payload: EvaluationResultPayload): Observable<EvaluationResultResponse> {
    return this.evaluationResultDal.getNextIdentifier$().pipe(
      switchMap(identifierResponse => {
        const identifier = identifierResponse.evaluationResult?.identifier ?? 0;
        return this.evaluationResultDal.addEvaluationResult$({
          assessmentResult: {
            identifier,
            assessmentIdentifier: payload.assessmentIdentifier,
            isAccepted: payload.isAccepted,
            mandatoryPointsEarned: payload.mandatoryPointsEarned,
            mandatoryThreshold: payload.mandatoryThreshold,
            optionalPointsEarned: payload.optionalPointsEarned,
            optionalRequired: payload.optionalRequired,
            onceFailedCount: payload.onceFailedCount,
            createdDate: new Date(),
            updatedDate: new Date()
          }
        });
      })
    );
  }
}
