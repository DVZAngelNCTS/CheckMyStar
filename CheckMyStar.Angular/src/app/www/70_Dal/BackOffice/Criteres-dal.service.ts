import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { CriteriaStatusResponse } from '../../50_Responses/BackOffice/CriteriaStatus.response';
import { CriteriaDetailsResponse } from '../../50_Responses/BackOffice/CriteriaDetail.reposne';

@Injectable({
  providedIn: 'root'
})
export class CriteresDalService {
  private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {    
  }

  getStarCriterias$(): Observable<CriteriaStatusResponse> {
    return this.http.post<CriteriaStatusResponse>(`${this.apiUrl}/Criteria/getstarcriteriastatus`, {});
  }

  getStarCriteriaDetails$(): Observable<CriteriaDetailsResponse> {
    return this.http.post<CriteriaDetailsResponse>(`${this.apiUrl}/Criteria/getstarcriteriadetails`, {});
  }
}
