import { Injectable } from '@angular/core';
import { AccommodationDalService } from '../../70_Dal/BackOffice/Accommodation-dal.service';
import { AccommodationModel } from '../../20_Models/BackOffice/Accommodation.model';
import { AccommodationSaveRequest } from '../../40_Requests/BackOffice/Accommodation-save.request';

@Injectable({
  providedIn: 'root'
})
export class AccommodationBllService {
  constructor(private accommodationDal: AccommodationDalService) {}

  getNextIdentifier$() {
    return this.accommodationDal.getNextIdentifier$();
  }

  createAccommodation$(accommodation: AccommodationModel) {
    return this.accommodationDal.createAccommodation$(accommodation);
  }

  updateAccommodation$(accommodation: AccommodationModel) {
    return this.accommodationDal.updateAccommodation$(accommodation);
  }

  enabledAccommodation$(accommodation: AccommodationModel) {
        const request = { accommodation: accommodation } as AccommodationSaveRequest;

    return this.accommodationDal.enabledAccommodation$(request);
  }
}
