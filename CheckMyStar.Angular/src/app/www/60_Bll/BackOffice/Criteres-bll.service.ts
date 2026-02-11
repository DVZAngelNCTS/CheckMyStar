import { Injectable } from '@angular/core';
import { CriteresDalService } from '../../70_Dal/BackOffice/Criteres-dal.service';

@Injectable({
  providedIn: 'root'
})
export class CriteresBllService {
  constructor(private criteriaDal: CriteresDalService) {}

  getStarCriterias$() {
    return this.criteriaDal.getStarCriterias$();
  }

  getStarCriteriaDetails$() {
    return this.criteriaDal.getStarCriteriaDetails$();
  }
}
