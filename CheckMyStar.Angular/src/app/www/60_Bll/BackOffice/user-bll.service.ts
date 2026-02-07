import { Injectable } from '@angular/core';
import { UserDalService } from '../../70_Dal/BackOffice/User-dal.service';
import { UserGetRequest } from '../../40_Requests/BackOffice/User-get.request';
import { UserModel } from '../../20_Models/Common/User.model';
import { UserSaveRequest } from '../../40_Requests/BackOffice/User-save.request';
import { UserDeleteRequest } from '../../40_Requests/BackOffice/User-delete.request';

@Injectable({
  providedIn: 'root'
})
export class UserBllService {
  constructor(private userDal: UserDalService) {
  }

  getUsers$(lastName?: string, firstName?: string, society?: string, email?: string, phone?: string, address?: string, role?: number) {
    const request = { 
      lastName: lastName,
      firstName: firstName,
      society: society,
      email: email,
      phone: phone,
      address: address,
      role: role
    } as UserGetRequest;

    return this.userDal.getUsers$(request);
  }

  addUser$(user: UserModel) {
    const request = { user: user } as UserSaveRequest;

    return this.userDal.addUser$(request);
  }

  updateUser$(user: UserModel) {
    const request = { user: user } as UserSaveRequest;

    return this.userDal.updateUser$(request);
  }

  deleteUser$(identifier: number) {
    const request = { identifier: identifier } as UserDeleteRequest;

    return this.userDal.deleteUser$(request);
  }

  getNextIdentifier$() {
    return this.userDal.getNextIdentifier$();
  }
}