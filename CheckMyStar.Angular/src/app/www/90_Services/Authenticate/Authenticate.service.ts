import { Injectable } from '@angular/core';
import { LoginBllService } from '../../60_Bll/FrontOffice/Login-bll.service';
import { Router } from '@angular/router';
import { UserModel } from '../../20_Models/Common/User.model';

@Injectable({
  providedIn: 'root'
})
export class AuthenticateService {
  constructor(private loginBllService: LoginBllService, private router: Router) {}

  login$(login: string, password: string) {
    return this.loginBllService.login$(login, password);
  }

  updatePassword$(login: string, oldPassword: string, newPassword: string) {
    return this.loginBllService.updatePassword$(login, oldPassword, newPassword);
  }

  generatePassword() {
    const length = 12;

    const upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    const lower = "abcdefghijklmnopqrstuvwxyz";
    const numbers = "0123456789";
    const symbols = "!@#$%^&*()_+-=[]{};':\"\\|,.<>/?";

    // 1) On garantit au moins un de chaque
    let pwd = "";
    pwd += upper[Math.floor(Math.random() * upper.length)];
    pwd += lower[Math.floor(Math.random() * lower.length)];
    pwd += numbers[Math.floor(Math.random() * numbers.length)];
    pwd += symbols[Math.floor(Math.random() * symbols.length)];

    // 2) On complète avec un mélange aléatoire
    const all = upper + lower + numbers + symbols;

    for (let i = pwd.length; i < length; i++) {
      pwd += all[Math.floor(Math.random() * all.length)];
    }

    // 3) On mélange le mot de passe pour éviter un pattern fixe
    pwd = pwd.split('').sort(() => Math.random() - 0.5).join('');

    return pwd;
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
