import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { CriteriaStatusResponse } from '../../50_Responses/BackOffice/CriteriaStatus.response';
import { CriteriaDetailsResponse } from '../../50_Responses/BackOffice/CriteriaDetail.reposne';
import { CreateCriterionRequest } from '../../20_Models/BackOffice/Criteres.model';
import { UpdateCriterionRequest } from '../../20_Models/BackOffice/Criteres.model';

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
  
createCriterion$(request: CreateCriterionRequest): Observable<any> {
  return this.http.post(`${this.apiUrl}/Criteria/addcriterion`, request);
}

  deleteCriterion$(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/Criteria/${id}`);
  }

  updateCriterion$(id: number, request: UpdateCriterionRequest): Observable<any> {
    return this.http.put(`${this.apiUrl}/Criteria/${id}`, request);
  }
}
