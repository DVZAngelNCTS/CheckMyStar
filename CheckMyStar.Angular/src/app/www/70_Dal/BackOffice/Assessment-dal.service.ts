import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { CreateAssessmentRequest } from '../../40_Requests/BackOffice/Assessment-create.request';
import { AssessmentResponse } from '../../50_Responses/BackOffice/Assessment.response';

@Injectable({
  providedIn: 'root'
})
export class AssessmentDalService {
  private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {}

  createAssessment$(request: CreateAssessmentRequest): Observable<AssessmentResponse> {
    return this.http.post<AssessmentResponse>(`${this.apiUrl}/Assessment/createassessment`, request);
  }

  deleteAssessment$(id: number): Observable<AssessmentResponse> {
    return this.http.delete<AssessmentResponse>(`${this.apiUrl}/Assessment/deleteassessment/${id}`);
  }
}
