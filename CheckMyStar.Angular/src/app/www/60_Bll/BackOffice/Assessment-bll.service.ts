import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AssessmentDalService } from '../../70_Dal/BackOffice/Assessment-dal.service';
import { CreateAssessmentRequest, UpdateAssessmentRequest } from '../../40_Requests/BackOffice/Assessment-create.request';
import { AssessmentResponse } from '../../50_Responses/BackOffice/Assessment.response';

@Injectable({
  providedIn: 'root'
})
export class AssessmentBllService {
  constructor(private assessmentDal: AssessmentDalService) {}

  createAssessment$(request: CreateAssessmentRequest): Observable<AssessmentResponse> {
    return this.assessmentDal.createAssessment$(request);
  }

  updateAssessment$(request: UpdateAssessmentRequest): Observable<AssessmentResponse> {
    return this.assessmentDal.updateAssessment$(request);
  }

  deleteAssessment$(id: number): Observable<AssessmentResponse> {
    return this.assessmentDal.deleteAssessment$(id);
  }
}
