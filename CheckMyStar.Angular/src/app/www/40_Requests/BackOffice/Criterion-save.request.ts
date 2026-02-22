import { StarCriterionModel } from '../../20_Models/BackOffice/Criterion.model';
import { StarLevelCriterionModel } from '../../20_Models/BackOffice/StarLevelCriterion.model';

export interface CriterionSaveRequest {
  starCriterion: StarCriterionModel;
  starLevelCriterion: StarLevelCriterionModel;
}