import { Injectable } from '@angular/core';
import { AddressDalService } from '../../70_Dal/BackOffice/Address-dal.service';
import { AddressModel } from '../../20_Models/Common/Address.model';

@Injectable({
  providedIn: 'root'
})
export class AddressBllService {
  constructor(private addressDal: AddressDalService) {
  }

  getNextIdentifier$() {
    return this.addressDal.getNextIdentifier$();
  }

  addAddress$(payload: { address: AddressModel }) {
    return this.addressDal.addAddress$(payload);
  }
}