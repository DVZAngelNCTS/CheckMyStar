import { Injectable } from '@angular/core';
import { LoginDalService } from '../../50_Dal/FrontOffice/login-dal.service';
import { LoginGetRequest } from '../../40_Requests/FrontOffice/login-get.request';

@Injectable()
export class LoginBllService {
  constructor(private loginDal: LoginDalService) {
  }

  login$(login: string, password: string) {
    return this.loginDal.login$({ Login: login, Password: password } as LoginGetRequest);
  }
}