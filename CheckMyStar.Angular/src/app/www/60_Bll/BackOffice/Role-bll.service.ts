import { Injectable } from '@angular/core';
import { RoleDalService } from '../../50_Dal/BackOffice/Role-dal.service';
import { RoleGetRequest } from '../../40_Requests/BackOffice/Role-get.request';
import { RoleModel } from '../../20_Models/BackOffice/Role.model';
import { RoleSaveRequest } from '../../40_Requests/BackOffice/Role-save.request';
import { RoleDeleteRequest } from '../../40_Requests/BackOffice/Role-delete.request';

@Injectable({
  providedIn: 'root'
})
export class RoleBllService {
  constructor(private roleDal: RoleDalService) {
  }

  getRoles$(name?: string) {
    const request = { name: name } as RoleGetRequest;

    return this.roleDal.getRoles$(request);
  }

  saveRole$(role: RoleModel) {
    const request = { role: role } as RoleSaveRequest;

    return this.roleDal.saveRole$(request);
  }

  deleteRole$(identifier: number) {
    const request = { identifier: identifier } as RoleDeleteRequest;

    return this.roleDal.deleteRole$(request);
  }
}