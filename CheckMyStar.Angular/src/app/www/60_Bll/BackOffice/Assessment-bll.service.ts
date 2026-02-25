import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AssessmentDalService } from '../../70_Dal/BackOffice/Assessment-dal.service';
import { AssessmentSaveRequest } from '../../40_Requests/BackOffice/Assessment-save.request';
import { AssessmentResponse } from '../../50_Responses/BackOffice/Assessment.response';
import { AssessmentCriteriaResponse } from '../../50_Responses/BackOffice/AssessmentCriteria.response';
import { AssessmentDeleteRequest } from '../../40_Requests/BackOffice/Assessment-delete.request';
import { AssessmentModel } from '../../20_Models/BackOffice/Assessment.model';

@Injectable({
  providedIn: 'root'
})
export class AssessmentBllService {
  constructor(private assessmentDal: AssessmentDalService) {}

  addAssessment$(model: AssessmentModel): Observable<AssessmentResponse> {
    const request = { assessment: model } as AssessmentSaveRequest;

    return this.assessmentDal.addAssessment$(request);    
  }

  updateAssessment$(model: AssessmentModel): Observable<AssessmentResponse> {
    const request = { assessment: model } as AssessmentSaveRequest;

    return this.assessmentDal.updateAssessment$(request);
  }

  deleteAssessment$(identifier: number) {
    const request = { identifier: identifier } as AssessmentDeleteRequest;

    return this.assessmentDal.deleteAssessment$(request);
  }

  getAssessment$(identifier: number): Observable<AssessmentResponse> {
    return this.assessmentDal.getAssessment$(identifier);
  }

  getAssessmentCriteria$(assessmentIdentifier: number): Observable<AssessmentCriteriaResponse> {
    return this.assessmentDal.getAssessmentCriteria$(assessmentIdentifier);
  }

  getAssessmentByFolder$(folderIdentifier: number): Observable<AssessmentResponse> {
    return this.assessmentDal.getAssessmentByFolder$(folderIdentifier);
  }
}
