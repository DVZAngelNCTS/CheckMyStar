import { Injectable } from '@angular/core';
import { AddressDalService } from '../../70_Dal/BackOffice/Address-dal.service';

@Injectable({
  providedIn: 'root'
})
export class AddressBllService {
  constructor(private addressDal: AddressDalService) {
  }

  getNextIdentifier$() {
    return this.addressDal.getNextIdentifier$();
  }
}