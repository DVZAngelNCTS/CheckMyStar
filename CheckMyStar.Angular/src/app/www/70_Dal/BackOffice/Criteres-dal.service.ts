import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { CriteriaStatusResponse } from '../../50_Responses/BackOffice/CriteriaStatus.response';
import { CriteriaDetailsResponse } from '../../50_Responses/BackOffice/CriteriaDetail.reposne';
import { CriterionSaveRequest } from '../../40_Requests/BackOffice/Criterion-save.request';
import { CriterionUpdateRequest } from '../../40_Requests/BackOffice/Criterion-update.request';
import { CriterionDeleteRequest } from '../../40_Requests/BackOffice/Criterion-delete.request';
import { BaseResponse } from '../../50_Responses/BaseResponse';

@Injectable({
  providedIn: 'root'
})
export class CriteresDalService {
  private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {    
  }

  getStarCriterias$(): Observable<CriteriaStatusResponse> {
    return this.http.get<CriteriaStatusResponse>(`${this.apiUrl}/Criteria/getstarcriteriastatus`, {});
  }

  getStarCriteriaDetails$(): Observable<CriteriaDetailsResponse> {
    return this.http.get<CriteriaDetailsResponse>(`${this.apiUrl}/Criteria/getstarcriteriadetails`, {});
  }
  
  addCriterion$(request: CriterionSaveRequest) {
    return this.http.post<BaseResponse>(`${this.apiUrl}/Criteria/addcriterion`, request);
  }

  deleteCriterion$(request: CriterionDeleteRequest) {
    let params = new HttpParams();

    Object.keys(request).forEach(key => {
      const value = (request as any)[key];
      if (value !== undefined && value !== null) {
        params = params.set(key, value);
      }
    });

    return this.http.delete<BaseResponse>(`${this.apiUrl}/Criteria/deletecriterion`, { params });
  }

  updateCriterion$(request: CriterionUpdateRequest) {
    return this.http.put<BaseResponse>(`${this.apiUrl}/Criteria/updatecriterion`, request);
  }
}
