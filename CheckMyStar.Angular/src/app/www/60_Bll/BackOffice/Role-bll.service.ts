import { Injectable } from '@angular/core';
import { RoleDalService } from '../../50_Dal/BackOffice/Role-dal.service';
import { RoleGetRequest } from '../../40_Requests/BackOffice/Role-get.request';

@Injectable({
  providedIn: 'root'
})
export class RoleBllService {
  constructor(private roleDal: RoleDalService) {
  }

  getRole$(name?: string) {
    const request = { name : name } as RoleGetRequest
    return this.roleDal.getRoles$(request);
  }
}