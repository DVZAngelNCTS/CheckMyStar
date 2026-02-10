// www/70_Dal/BackOffice/Criteres-dal.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { StarCriteria, StarCriteriaDetail } from '../../20_Models/BackOffice/Criteres.model';
import { Environment } from '../../../../Environment/environment';

@Injectable({
  providedIn: 'root'
})
export class CriteresDalService {
  private readonly baseUrl = Environment.ApiUrl; 
  // ex: "http://checkmystar.apis.local:5109/api"

  constructor(private http: HttpClient) {}

  getStarCriteria(): Observable<StarCriteria[]> {
    // NE PAS rajouter /api ici
    return this.http.post<StarCriteria[]>(`${this.baseUrl}/criteres`, {});
  }

  getStarCriteriaDetails(): Observable<StarCriteriaDetail[]> {
    return this.http.post<StarCriteriaDetail[]>(`${this.baseUrl}/criteres/details`, {});
  }
}
