import { Injectable } from '@angular/core';
import { CriteresDalService } from '../../70_Dal/BackOffice/Criteres-dal.service';
import { CriterionSaveRequest } from '../../40_Requests/BackOffice/Criterion-save.request';
import { CriterionUpdateRequest } from '../../40_Requests/BackOffice/Criterion-update.request';
import { CriterionModel } from '../../20_Models/BackOffice/Criterion.model';
import { StarLevelModel } from '../../20_Models/BackOffice/StarLevel.model';
import { StarLevelCriterionModel } from '../../20_Models/BackOffice/StarLevelCriterion.model';

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

  addCriterion$(criterion: CriterionModel, starLevelCriterion: StarLevelCriterionModel) {
    const request = { starLevel: criterion, starLevelCriterion } as CriterionSaveRequest;

    return this.criteriaDal.addCriterion$(request);
  }

  deleteCriterion$(id: number) {
    return this.criteriaDal.deleteCriterion$(id);
  }

  updateCriterion$(criterion: CriterionModel, starLevel: StarLevelModel, starLevelCriterion: StarLevelCriterionModel) {
    const request = { criterion, starLevel, starLevelCriterion } as CriterionUpdateRequest;

    return this.criteriaDal.updateCriterion$(request);
  }
}
