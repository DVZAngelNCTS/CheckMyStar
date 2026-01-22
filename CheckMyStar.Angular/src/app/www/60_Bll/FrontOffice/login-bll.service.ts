import { Injectable } from '@angular/core';
import { LoginDalService } from '../../70_Dal/FrontOffice/Login-dal.service';
import { LoginGetRequest } from '../../40_Requests/FrontOffice/Login-get.request';

@Injectable({
  providedIn: 'root'
})
export class LoginBllService {
  constructor(private loginDal: LoginDalService) {
  }

  login$(login: string, password: string) {
    return this.loginDal.login$({ login: login, password: password } as LoginGetRequest);
  }
}