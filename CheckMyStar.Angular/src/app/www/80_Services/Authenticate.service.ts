import { Injectable } from '@angular/core';
import { LoginBllService } from '../60_Bll/FrontOffice/login-bll.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthenticateService {
  constructor(private loginBllService: LoginBllService, private router: Router) {}

  async login(login: string, password: string): Promise<boolean> {
    try {
      const result = await this.loginBllService.login$(login, password).toPromise();
      if (result) {
        localStorage.setItem('token', result.token); // Stocke le token
        this.router.navigate(['/home']);
        return true;
      }
    } catch (error) {
      console.error('Login failed', error);
    }
    return false;
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('token');
  }

  logout(): void {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}