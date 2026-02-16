import { Injectable } from '@angular/core';
import { CriteresDalService } from '../../70_Dal/BackOffice/Criteres-dal.service';
import { CreateCriterionRequest } from '../../20_Models/BackOffice/Criteres.model';
import { Observable } from 'rxjs';
import { UpdateCriterionRequest } from '../../20_Models/BackOffice/Criteres.model';

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

  createCriterion$(request: CreateCriterionRequest): Observable<any> {
    return this.criteriaDal.createCriterion$(request);
  }

  deleteCriterion$(id: number): Observable<any> {
    return this.criteriaDal.deleteCriterion$(id);
  }

  updateCriterion$(id: number, request: UpdateCriterionRequest): Observable<any> {
    return this.criteriaDal.updateCriterion$(id, request);
  }
}
