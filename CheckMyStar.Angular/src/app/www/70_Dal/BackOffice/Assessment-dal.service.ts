import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { AssessmentSaveRequest } from '../../40_Requests/BackOffice/Assessment-save.request';
import { AssessmentDeleteRequest } from '../../40_Requests/BackOffice/Assessment-delete.request';
import { AssessmentResponse } from '../../50_Responses/BackOffice/Assessment.response';
import { BaseResponse } from '../../50_Responses/BaseResponse';

@Injectable({
  providedIn: 'root'
})
export class AssessmentDalService {
  private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {}

  addAssessment$(request: AssessmentSaveRequest): Observable<AssessmentResponse> {
    return this.http.post<AssessmentResponse>(`${this.apiUrl}/Assessment/addassessment`, request);
  }

  deleteAssessment$(request: AssessmentDeleteRequest): Observable<BaseResponse> {
    let params = new HttpParams();

    Object.keys(request).forEach(key => {
      const value = (request as any)[key];
      if (value !== undefined && value !== null) {
        params = params.set(key, value);
      }
    });

    return this.http.request<BaseResponse>('delete', `${this.apiUrl}/Assessment/deleteassessment`, { params });
  }
}
