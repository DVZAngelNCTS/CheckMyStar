import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { UserModel } from '../../20_Models/FrontOffice/User.model';

@Injectable({
  providedIn: 'root'
})
export class UserDalService {
   private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {
  }

  getUsers$(): Observable<UserModel[]> {
    return this.http.get<UserModel[]>(`${this.apiUrl}/authenticate/getUsers`);
  }
}