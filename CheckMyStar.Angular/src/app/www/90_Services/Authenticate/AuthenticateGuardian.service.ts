import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthenticateService } from './Authenticate.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticateGuardian implements CanActivate {
  constructor(private authenticateService: AuthenticateService, private router: Router) {}

  canActivate(): boolean {
    if (this.authenticateService.isAuthenticated()) {
      return true;
    }

    this.router.navigate(['/login']);
    return false;
  }
}