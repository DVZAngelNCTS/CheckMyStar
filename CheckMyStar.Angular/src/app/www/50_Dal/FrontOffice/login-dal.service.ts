import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginGetRequest } from '../../40_Requests/FrontOffice/login-get.request';
import { LoginModel } from '../../20_Models/FrontOffice/login.model';
import { Environment } from '../../../../Environment/environment';

@Injectable({
  providedIn: 'root'
})
export class LoginDalService {
   private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {
  }

  login$(request: LoginGetRequest): Observable<LoginModel> {
    return this.http.post<LoginModel>('${this.apiUrl}/authenticate/login', request);
  }
}