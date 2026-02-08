import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { DashboardResponse } from '../../50_Responses/BackOffice/Dashboard.response';

@Injectable({
  providedIn: 'root'
})
export class DashboardDalService {
   private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {
  }

  getDashboard$(): Observable<DashboardResponse> {
    return this.http.post<DashboardResponse>(`${this.apiUrl}/Dashboard/getdashboard`, {});
  }
}