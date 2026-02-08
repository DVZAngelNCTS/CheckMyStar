import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { UserGetRequest } from '../../40_Requests/BackOffice/User-get.request';
import { UserSaveRequest } from '../../40_Requests/BackOffice/User-save.request';
import { UserDeleteRequest } from '../../40_Requests/BackOffice/User-delete.request';
import { UsersResponse } from '../../50_Responses/BackOffice/Users.response';
import { BaseResponse } from '../../50_Responses/BaseResponse';
import { UserResponse } from '../../50_Responses/BackOffice/User.response';

@Injectable({
  providedIn: 'root'
})
export class UserDalService {
   private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {
  }

  getUsers$(request: UserGetRequest): Observable<UsersResponse> {
    return this.http.post<UsersResponse>(`${this.apiUrl}/User/getusers`, request);
  }

  addUser$(request: UserSaveRequest) {
    return this.http.post<BaseResponse>(`${this.apiUrl}/User/adduser`, request);
  }
  
  updateUser$(request: UserSaveRequest) {
    return this.http.post<BaseResponse>(`${this.apiUrl}/User/updateuser`, request);
  }
  
  deleteUser$(request: UserDeleteRequest) {
        return this.http.post<BaseResponse>(`${this.apiUrl}/User/deleteuser`, request);
  }

  getNextIdentifier$(): Observable<UserResponse> {
    return this.http.post<UserResponse>(`${this.apiUrl}/User/getnextidentifier`, {});
  }
}