import { Injectable } from '@angular/core';
import { AddressDalService } from '../../70_Dal/BackOffice/Address-dal.service';
import { AddressModel } from '../../20_Models/Common/Address.model';
import { GeolocationGetRequest } from '../../40_Requests/Common/Geolocation-get.request';
import { AddressGetRequest } from '../../40_Requests/BackOffice/Address-get.request';
import { AddressSaveRequest } from '../../40_Requests/BackOffice/Address-save.request';
import { AddressDeleteRequest } from '../../40_Requests/BackOffice/Address-delete.request';

@Injectable({
  providedIn: 'root'
})
export class AddressBllService {
  constructor(private addressDal: AddressDalService) {
  }

  getNextIdentifier$() {
    return this.addressDal.getNextIdentifier$();
  }

  getAddresses$(number?: string, addressLine?: string, city?: string, zipCode?: string, region?: string, countryIdentifier?: number) {
    const request = { 
      number: number,
      addressLine: addressLine,
      city: city,
      zipCode: zipCode,
      region: region,
      countryIdentifier: countryIdentifier
    } as AddressGetRequest;

    return this.addressDal.getAddresses$(request);
  }

  addAddress$(address: AddressModel) {
    const request = { address: address } as AddressSaveRequest;

    return this.addressDal.addAddress$(request);
  }

  updateAddress$(address: AddressModel) {
    const request = { address: address } as AddressSaveRequest;

    return this.addressDal.updateAddress$(request);
  }

  deleteAddress$(identifier: number) {
    const request = { identifier: identifier } as AddressDeleteRequest;

    return this.addressDal.deleteAddresses$(request);
  }

  searchAddress$(address: string) {
    const request = { 
      address: address
    } as GeolocationGetRequest;

    return this.addressDal.searchAddress$(request);
  }
}