import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { CriteresDalService } from '../../70_Dal/BackOffice/Criteres-dal.service';
import { StarCriteria, StarCriteriaDetail } from '../../20_Models/BackOffice/Criteres.model';

@Injectable({
  providedIn: 'root'
})
export class CriteresBllService {
  constructor(private dal: CriteresDalService) {}

  getStarCriteria(): Observable<StarCriteria[]> {
    return this.dal.getStarCriteria();
  }

  getStarCriteriaDetails(): Observable<StarCriteriaDetail[]> {
    return this.dal.getStarCriteriaDetails();
  }
}
