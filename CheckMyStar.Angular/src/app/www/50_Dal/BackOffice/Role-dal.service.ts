import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { RoleModel } from '../../20_Models/BackOffice/Role.model'
import { RoleGetRequest } from '../../40_Requests/BackOffice/Role-get.request';

@Injectable({
  providedIn: 'root'
})
export class RoleDalService {
   private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {
  }

  getRoles$(request: RoleGetRequest): Observable<RoleModel[]> {
    return this.http.post<RoleModel[]>(`${this.apiUrl}/Role/getroles`, request);
  }
}
