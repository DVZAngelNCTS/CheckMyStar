import { Injectable } from '@angular/core';
import { LoginBllService } from '../../60_Bll/FrontOffice/login-bll.service';
import { Router } from '@angular/router';

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
    localStorage.removeItem('user');
    this.router.navigate(['/login']);
  }
}
