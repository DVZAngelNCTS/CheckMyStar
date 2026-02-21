import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { ActivityResponse } from '../../50_Responses/BackOffice/Activity.response';
import { ActivityGetRequest } from '../../40_Requests/BackOffice/Activity-get.request';

@Injectable({
  providedIn: 'root'
})
export class ActivityDalService {
   private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {
  }

  getActivities$(request: ActivityGetRequest): Observable<ActivityResponse> {
    let params = new HttpParams();

    Object.keys(request).forEach(key => {
      const value = (request as any)[key];
      if (value !== undefined && value !== null) {
        params = params.set(key, value);
      }
    });

    return this.http.get<ActivityResponse>(`${this.apiUrl}/Activity/getactivities`, { params });
  }
}