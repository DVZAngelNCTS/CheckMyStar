import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { UserGetRequest } from '../../40_Requests/BackOffice/User-get.request';
import { UserSaveRequest } from '../../40_Requests/BackOffice/User-save.request';
import { UserDeleteRequest } from '../../40_Requests/BackOffice/User-delete.request';
import { UsersResponse } from '../../50_Responses/BackOffice/Users.response';
import { BaseResponse } from '../../50_Responses/BaseResponse';
import { UserResponse } from '../../50_Responses/BackOffice/User.response';
import { UserEvolutionsResponse } from '../../50_Responses/BackOffice/UserEvolutions.response';

@Injectable({
  providedIn: 'root'
})
export class UserDalService {
   private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {
  }

  getUsers$(request: UserGetRequest): Observable<UsersResponse> {
    let params = new HttpParams();

    Object.keys(request).forEach(key => {
      const value = (request as any)[key];
      if (value !== undefined && value !== null) {
        params = params.set(key, value);
      }
    });

    return this.http.get<UsersResponse>(`${this.apiUrl}/User/getusers`, { params });
  }

  addUser$(request: UserSaveRequest) {   
    alert(JSON.stringify(request.user));
    return this.http.post<BaseResponse>(`${this.apiUrl}/User/adduser`, request);
  }
  
  updateUser$(request: UserSaveRequest) {
    return this.http.put<BaseResponse>(`${this.apiUrl}/User/updateuser`, request);
  }
  
  deleteUser$(request: UserDeleteRequest) {
    let params = new HttpParams();

    Object.keys(request).forEach(key => {
      const value = (request as any)[key];
      if (value !== undefined && value !== null) {
        params = params.set(key, value);
      }
    });

    return this.http.delete<BaseResponse>(`${this.apiUrl}/User/deleteuser`, { params });
  }

  getNextIdentifier$(): Observable<UserResponse> {
    return this.http.get<UserResponse>(`${this.apiUrl}/User/getnextidentifier`, {});
  }

  getUserEvolutions$(): Observable<UserEvolutionsResponse> {
    return this.http.get<UserEvolutionsResponse>(`${this.apiUrl}/User/getuserevolutions`, {});
  }
}