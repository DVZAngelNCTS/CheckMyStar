import { Injectable } from '@angular/core';
import { HttpClient, HttpParams} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { SocietiesResponse } from '../../50_Responses/BackOffice/Societies.response';
import { SocietySaveRequest } from '../../40_Requests/BackOffice/Society-save.request';
import { BaseResponse } from '../../50_Responses/BaseResponse';
import { SocietyResponse} from '../../50_Responses/BackOffice/Society.response';
import { SocietyDeleteRequest } from '../../40_Requests/BackOffice/Society-delete.request';
import { SocietyGetRequest } from '../../40_Requests/BackOffice/Society-get.request';

@Injectable({ providedIn: 'root' })
export class SocietyDalService {
  private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {}

  getNextIdentifier$(): Observable<SocietyResponse> {
    return this.http.get<SocietyResponse>(`${this.apiUrl}/Society/getnextidentifier`, {});
  }

  getSocieties$(request: SocietyGetRequest): Observable<SocietiesResponse> {
    let params = new HttpParams();

    Object.keys(request).forEach(key => {
      const value = (request as any)[key];
      if (value !== undefined && value !== null) {
        params = params.set(key, value);
      }
    });

    return this.http.get<SocietiesResponse>(`${this.apiUrl}/Society/getsocieties`, { params });
  }

  getSociety$(request: SocietyGetRequest): Observable<SocietyResponse> {
    let params = new HttpParams();

    Object.keys(request).forEach(key => {
      const value = (request as any)[key];
      if (value !== undefined && value !== null) {
        params = params.set(key, value);
      }
    });

    return this.http.get<SocietyResponse>(`${this.apiUrl}/Society/getsociety`, { params });
  }

  addSociety$(request: SocietySaveRequest) {
    return this.http.post<BaseResponse>(`${this.apiUrl}/Society/addsociety`, request);
  }

  updateSociety$(request: SocietySaveRequest) {
    return this.http.put<BaseResponse>(`${this.apiUrl}/Society/updatesociety`, request);
  }
  
  deleteSociety$(request: SocietyDeleteRequest) {
    let params = new HttpParams();

    Object.keys(request).forEach(key => {
      const value = (request as any)[key];
      if (value !== undefined && value !== null) {
        params = params.set(key, value);
      }
    });

    return this.http.delete<BaseResponse>(`${this.apiUrl}/Society/deletesociety`, { params });
  }

  enabledSociety$(request: SocietySaveRequest) {
    return this.http.put<BaseResponse>(`${this.apiUrl}/Society/enabledsociety`, request);
  }  
}
