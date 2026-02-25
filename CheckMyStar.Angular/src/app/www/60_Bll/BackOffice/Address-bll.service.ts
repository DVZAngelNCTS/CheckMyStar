import { Injectable } from '@angular/core';
import { AddressDalService } from '../../70_Dal/BackOffice/Address-dal.service';
import { AddressModel } from '../../20_Models/Common/Address.model';
import { GeolocationGetRequest } from '../../40_Requests/Common/Geolocation-get.request';

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

  updateAddress$(payload: { address: AddressModel }) {
    return this.addressDal.updateAddress$(payload);
  }

  searchAddress$(address: string) {
    const request = { 
      address: address
    } as GeolocationGetRequest;

    return this.addressDal.searchAddress$(request);
  }
}