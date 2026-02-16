import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginGetRequest } from '../../40_Requests/FrontOffice/Login-get.request';
import { Environment } from '../../../../Environment/environment';
import { LoginResponse } from '../../50_Responses/FrontOffice/LoginResponse';
import { PasswordGetRequest } from '../../40_Requests/FrontOffice/Password-get.request';

@Injectable({
  providedIn: 'root'
})
export class LoginDalService {
   private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {
  }

  login$(request: LoginGetRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/authenticate/login`, request);
  }

  updatePassword$(request: PasswordGetRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/authenticate/updatepassword`, request);
  }

  refresh$(refreshToken: string): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/authenticate/refresh`, { refreshToken });
  }
}