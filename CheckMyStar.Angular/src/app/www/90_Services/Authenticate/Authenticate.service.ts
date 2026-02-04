import { Injectable } from '@angular/core';
import { LoginBllService } from '../../60_Bll/FrontOffice/Login-bll.service';
import { Router } from '@angular/router';
import { UserModel } from '../../20_Models/Common/User.model';

@Injectable({
  providedIn: 'root'
})
export class AuthenticateService {
  constructor(private loginBllService: LoginBllService, private router: Router) {}

  login(login: string, password: string) {
    return this.loginBllService.login$(login, password);
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('token');
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.router.navigate(['/login']);
  }

  getCurrentUser(): UserModel | null {
    const userJson = localStorage.getItem('user');

    if (!userJson) return null;
    
    try {
      return JSON.parse(userJson) as UserModel;
    } catch {
      return null;
    }
  }

  getAccessToken(): string | null {
    return localStorage.getItem('token');
  }

  getRefreshToken(): string | null {
    return localStorage.getItem('refreshToken');
  }

  setTokens(access: string, refresh: string) {
    localStorage.setItem('token', access);
    localStorage.setItem('refreshToken', refresh);
  }

  refreshToken$() {
    const refresh = this.getRefreshToken();
    return this.loginBllService.refresh$(refresh!);
  }
}
