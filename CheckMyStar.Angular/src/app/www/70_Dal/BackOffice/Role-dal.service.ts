import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { RoleGetRequest } from '../../40_Requests/BackOffice/Role-get.request';
import { RoleSaveRequest } from '../../40_Requests/BackOffice/Role-save.request';
import { RoleDeleteRequest } from '../../40_Requests/BackOffice/Role-delete.request';
import { RolesResponse } from '../../50_Responses/BackOffice/Roles.response';
import { BaseResponse } from '../../50_Responses/BaseResponse';
import { RoleResponse } from '../../50_Responses/BackOffice/Role.response';

@Injectable({
  providedIn: 'root'
})
export class RoleDalService {
   private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {
  }

  getRoles$(request: RoleGetRequest): Observable<RolesResponse> {
    return this.http.post<RolesResponse>(`${this.apiUrl}/Role/getroles`, request);
  }

  addRole$(request: RoleSaveRequest) {
    return this.http.post<BaseResponse>(`${this.apiUrl}/Role/addrole`, request);
  }

  updateRole$(request: RoleSaveRequest) {
    return this.http.post<BaseResponse>(`${this.apiUrl}/Role/updaterole`, request);
  }

  deleteRole$(request: RoleDeleteRequest) {
        return this.http.post<BaseResponse>(`${this.apiUrl}/Role/deleterole`, request);
  }

  getNextIdentifier$(): Observable<RoleResponse> {
    return this.http.post<RoleResponse>(`${this.apiUrl}/Role/getnextidentifier`, {});
  }
}
