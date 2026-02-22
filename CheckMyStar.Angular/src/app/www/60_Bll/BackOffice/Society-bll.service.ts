import { Injectable } from '@angular/core';
import { SocietyDalService } from '../../70_Dal/BackOffice/Society-dal.service';
import { SocietySaveRequest } from '../../40_Requests/BackOffice/Society-save.request';
import { SocietyModel } from '../../20_Models/BackOffice/Society.model';

@Injectable({ providedIn: 'root' })
export class SocietyBllService {
  constructor(private societyDal: SocietyDalService) {}

  getSocieties$() {
    return this.societyDal.getSocieties$();
  }

  addSociety$(society: SocietyModel) {    
    const request = { society: society } as SocietySaveRequest;

    return this.societyDal.addSociety$(request);
  }
}