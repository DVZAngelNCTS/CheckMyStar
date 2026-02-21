import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { SocietiesResponse } from '../../50_Responses/BackOffice/Societies.response';

@Injectable({ providedIn: 'root' })
export class SocietyDalService {
  private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {}

  getSocieties$(): Observable<SocietiesResponse> {
    return this.http.get<SocietiesResponse>(`${this.apiUrl}/Societies/getSocieties`, {});
  }

  addSociety$(payload: { societies: any[] } | any[] | any): Observable<any> {
    let body = payload;
    if (Array.isArray(payload)) {
      body = payload[0] ?? {};
    } else if (payload && 'societies' in payload) {
      body = payload.societies?.[0] ?? {};
    }
    return this.http.post<any>(`${this.apiUrl}/Societies/addSociety`, body);
  }
}