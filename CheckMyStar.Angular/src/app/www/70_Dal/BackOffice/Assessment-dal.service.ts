import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { AssessmentSaveRequest } from '../../40_Requests/BackOffice/Assessment-save.request';
import { AssessmentDeleteRequest } from '../../40_Requests/BackOffice/Assessment-delete.request';
import { AssessmentGetRequest } from '../../40_Requests/BackOffice/Assessment-get.request';
import { AssessmentGetByFolderRequest } from '../../40_Requests/BackOffice/Assessment-getByFolder.request';
import { AssessmentResponse } from '../../50_Responses/BackOffice/Assessment.response';
import { AssessmentCriteriaResponse } from '../../50_Responses/BackOffice/AssessmentCriteria.response';
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

  updateAssessment$(request: AssessmentSaveRequest): Observable<AssessmentResponse> {
    return this.http.put<AssessmentResponse>(`${this.apiUrl}/Assessment/updateassessment`, request);
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

  getAssessment$(identifier: number): Observable<AssessmentResponse> {
    const params = new HttpParams().set('identifier', identifier);
    return this.http.get<AssessmentResponse>(`${this.apiUrl}/Assessment/getassessment`, { params });
  }

  getAssessmentCriteria$(assessmentIdentifier: number): Observable<AssessmentCriteriaResponse> {
    const params = new HttpParams().set('assessmentIdentifier', assessmentIdentifier);
    return this.http.get<AssessmentCriteriaResponse>(`${this.apiUrl}/Assessment/getassessmentcriteria`, { params });
  }

  getAssessmentByFolder$(folderIdentifier: number): Observable<AssessmentResponse> {
    const params = new HttpParams().set('folderIdentifier', folderIdentifier);
    return this.http.get<AssessmentResponse>(`${this.apiUrl}/Assessment/getassessmentbyfolder`, { params });
  }
}
