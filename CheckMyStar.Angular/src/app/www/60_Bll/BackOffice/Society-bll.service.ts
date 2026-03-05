import { Injectable } from '@angular/core';
import { SocietyDalService } from '../../70_Dal/BackOffice/Society-dal.service';
import { SocietySaveRequest } from '../../40_Requests/BackOffice/Society-save.request';
import { SocietyModel } from '../../20_Models/BackOffice/Society.model';
import { SocietyDeleteRequest } from '../../40_Requests/BackOffice/Society-delete.request';
import { SocietyGetRequest} from '../../40_Requests/BackOffice/Society-get.request';

@Injectable({ providedIn: 'root' })
export class SocietyBllService {
  constructor(private societyDal: SocietyDalService) {}

  getNextIdentifier$() {
    return this.societyDal.getNextIdentifier$();
  }

  getSocieties$() {
    return this.societyDal.getSocieties$();
  }

  getSociety$(identifier: number) {
    const request = { identifier: identifier } as SocietyGetRequest;

    return this.societyDal.getSociety$(request);
  }

  addSociety$(society: SocietyModel) {    
    const request = { society: society } as SocietySaveRequest;

    return this.societyDal.addSociety$(request);
  }

  updateSociety$(society: SocietyModel) {
    const request = { society: society } as SocietySaveRequest;

    return this.societyDal.updateSociety$(request);
  }

  deleteSociety$(identifier: number) {
    const request = { identifier: identifier } as SocietyDeleteRequest;

    return this.societyDal.deleteSociety$(request);
  }  
}