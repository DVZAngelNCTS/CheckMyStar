import { Injectable } from '@angular/core';
import { CountryDalService } from '../../70_Dal/BackOffice/Country-dal.service';

@Injectable({
  providedIn: 'root'
})
export class CountryBllService {
  constructor(private countryDal: CountryDalService) {
  }

  getCountries$() {
    return this.countryDal.getCountries$();
  }
}