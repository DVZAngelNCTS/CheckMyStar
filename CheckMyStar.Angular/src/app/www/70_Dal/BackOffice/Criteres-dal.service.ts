import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { CriteriaStatusResponse } from '../../50_Responses/BackOffice/CriteriaStatus.response';
import { CriteriaDetailsResponse } from '../../50_Responses/BackOffice/CriteriaDetail.reposne';
import { CriterionSaveRequest } from '../../40_Requests/BackOffice/Criterion-save.request';
import { CriterionUpdateRequest } from '../../40_Requests/BackOffice/Criterion-update.request';
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

  deleteCriterion$(id: number) {
    return this.http.delete<BaseResponse>(`${this.apiUrl}/Criteria/${id}`);
  }

  updateCriterion$(request: CriterionUpdateRequest) {
    return this.http.put<BaseResponse>(`${this.apiUrl}/Criteria`, request);
  }
}
