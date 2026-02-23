import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AssessmentDalService } from '../../70_Dal/BackOffice/Assessment-dal.service';
import { AssessmentSaveRequest } from '../../40_Requests/BackOffice/Assessment-save.request';
import { AssessmentResponse } from '../../50_Responses/BackOffice/Assessment.response';
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

  deleteAssessment$(identifier: number) {
    const request = { identifier: identifier } as AssessmentDeleteRequest;

    return this.assessmentDal.deleteAssessment$(request);
  }
}
