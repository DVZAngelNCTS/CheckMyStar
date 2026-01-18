import { Injectable } from '@angular/core';
import { UserDalService } from '../../50_Dal/BackOffice/User-dal.service';

@Injectable({
  providedIn: 'root'
})
export class UserBllService {
  constructor(private userDal: UserDalService) {
  }

  getUsers$() {
    return this.userDal.getUsers$();
  }
}