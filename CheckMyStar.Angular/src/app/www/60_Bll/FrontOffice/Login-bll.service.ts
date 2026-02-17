import { Injectable } from '@angular/core';
import { LoginDalService } from '../../70_Dal/FrontOffice/Login-dal.service';
import { LoginGetRequest } from '../../40_Requests/FrontOffice/Login-get.request';
import { PasswordGetRequest } from '../../40_Requests/FrontOffice/Password-get.request';

@Injectable({
  providedIn: 'root'
})
export class LoginBllService {
  constructor(private loginDal: LoginDalService) {
  }

  login$(login: string, password: string) {
    return this.loginDal.login$({ login: login, password: password } as LoginGetRequest);
  }

  updatePassword$(login: string, oldPassword: string, newPassword: string) {
    return this.loginDal.updatePassword$({ login, oldPassword, newPassword } as PasswordGetRequest);
  }
  
  refresh$(refreshToken: string) {
    return this.loginDal.refresh$(refreshToken);
  }
}
