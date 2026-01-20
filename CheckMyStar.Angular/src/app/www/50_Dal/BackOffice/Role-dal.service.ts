import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { RoleModel } from '../../20_Models/BackOffice/Role.model'
import { RoleGetRequest } from '../../40_Requests/BackOffice/Role-get.request';
import { RoleSaveRequest } from '../../40_Requests/BackOffice/Role-save.request';
import { RoleDeleteRequest } from '../../40_Requests/BackOffice/Role-delete.request';

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

  saveRole$(request: RoleSaveRequest) {
    return this.http.post<boolean>(`${this.apiUrl}/Role/saverole`, request);
  }

  deleteRole$(request: RoleDeleteRequest) {
        return this.http.post<boolean>(`${this.apiUrl}/Role/deleterole`, request);
  }
}
