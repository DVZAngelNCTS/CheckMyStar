import { Injectable } from '@angular/core';
import { SocietyDalService } from '../../70_Dal/BackOffice/Society-dal.service';

@Injectable({ providedIn: 'root' })
export class SocietyBllService {
  constructor(private societyDal: SocietyDalService) {}

  getSocieties$() {
    return this.societyDal.getSocieties$();
  }

  addSociety$(payload: { societies: any[] }) {
    return this.societyDal.addSociety$(payload);
  }
}