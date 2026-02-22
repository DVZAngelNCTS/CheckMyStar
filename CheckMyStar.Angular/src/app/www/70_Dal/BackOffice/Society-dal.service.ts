import { Injectable } from '@angular/core';
import { HttpClient, HttpParams} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { SocietiesResponse } from '../../50_Responses/BackOffice/Societies.response';
import { SocietySaveRequest } from '../../40_Requests/BackOffice/Society-save.request';
import { BaseResponse } from '../../50_Responses/BaseResponse';

@Injectable({ providedIn: 'root' })
export class SocietyDalService {
  private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {}

  getSocieties$(): Observable<SocietiesResponse> {
    return this.http.get<SocietiesResponse>(`${this.apiUrl}/Societies/getSocieties`, {});
  }

  addSociety$(request: SocietySaveRequest): Observable<any> {
    return this.http.post<BaseResponse>(`${this.apiUrl}/Societies/addSociety`, request);
  }
}