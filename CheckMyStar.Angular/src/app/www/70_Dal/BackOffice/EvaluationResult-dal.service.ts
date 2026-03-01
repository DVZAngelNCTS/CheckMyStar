import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { EvaluationResultSaveRequest } from '../../40_Requests/BackOffice/EvaluationResult-save.request';
import { EvaluationResultResponse } from '../../50_Responses/BackOffice/EvaluationResult.response';
import { EvaluationResultsResponse } from '../../50_Responses/BackOffice/EvaluationResults.response';

@Injectable({
  providedIn: 'root'
})
export class EvaluationResultDalService {
  private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {}

  getNextIdentifier$(): Observable<EvaluationResultResponse> {
    return this.http.get<EvaluationResultResponse>(`${this.apiUrl}/AssessmentResult/getnextidentifier`);
  }

  addEvaluationResult$(request: EvaluationResultSaveRequest): Observable<EvaluationResultResponse> {
    return this.http.post<EvaluationResultResponse>(`${this.apiUrl}/AssessmentResult/addassessmentresult`, request);
  }

  getAssessmentResultsByFolder$(folderIdentifier: number): Observable<EvaluationResultsResponse> {
    const params = new HttpParams().set('folderIdentifier', folderIdentifier);
    return this.http.get<EvaluationResultsResponse>(`${this.apiUrl}/AssessmentResult/getassessmentresultsbyfolder`, { params });
  }
}
