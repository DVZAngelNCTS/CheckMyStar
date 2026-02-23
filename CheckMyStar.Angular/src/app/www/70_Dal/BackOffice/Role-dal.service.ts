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
    let params = new HttpParams();

    Object.keys(request).forEach(key => {
      const value = (request as any)[key];
      if (value !== undefined && value !== null) {
        params = params.set(key, value);
      }
    });

    return this.http.get<RolesResponse>(`${this.apiUrl}/Role/getroles`, { params});
  }

  addRole$(request: RoleSaveRequest) {
    return this.http.post<BaseResponse>(`${this.apiUrl}/Role/addrole`, request);
  }

  updateRole$(request: RoleSaveRequest) {
    return this.http.put<BaseResponse>(`${this.apiUrl}/Role/updaterole`, request);
  }

  deleteRole$(request: RoleDeleteRequest) {
    let params = new HttpParams();

    Object.keys(request).forEach(key => {
      const value = (request as any)[key];
      if (value !== undefined && value !== null) {
        params = params.set(key, value);
      }
    });

    return this.http.delete<BaseResponse>(`${this.apiUrl}/Role/deleterole`, { params });
  }


  getNextIdentifier$(): Observable<RoleResponse> {
    return this.http.get<RoleResponse>(`${this.apiUrl}/Role/getnextidentifier`, {});
  }
}
