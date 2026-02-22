import { Injectable } from '@angular/core';
import { CriteresDalService } from '../../70_Dal/BackOffice/Criteres-dal.service';
import { CriterionSaveRequest } from '../../40_Requests/BackOffice/Criterion-save.request';
import { CriterionUpdateRequest } from '../../40_Requests/BackOffice/Criterion-update.request';
import { StarCriterionModel } from '../../20_Models/BackOffice/Criterion.model';
import { StarLevelModel } from '../../20_Models/BackOffice/StarLevel.model';
import { StarLevelCriterionModel } from '../../20_Models/BackOffice/StarLevelCriterion.model';
import { CriterionDeleteRequest } from '../../40_Requests/BackOffice/Criterion-delete.request';

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

  addCriterion$(criterion: StarCriterionModel, starLevelCriterion: StarLevelCriterionModel) {
    const request = { starCriterion: criterion, starLevelCriterion } as CriterionSaveRequest;

    return this.criteriaDal.addCriterion$(request);
  }

  deleteCriterion$(identifier: number) {
    const request = { identifier: identifier } as CriterionDeleteRequest;

    return this.criteriaDal.deleteCriterion$(request);
  }

  updateCriterion$(criterion: StarCriterionModel, starLevel: StarLevelModel, starLevelCriterion: StarLevelCriterionModel) {
    const request = { criterion, starLevel, starLevelCriterion } as CriterionUpdateRequest;

    return this.criteriaDal.updateCriterion$(request);
  }
}
