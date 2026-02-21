import { CriterionModel } from '../../20_Models/BackOffice/Criterion.model';
import { StarLevelCriterionModel } from '../../20_Models/BackOffice/StarLevelCriterion.model';

export interface CriterionSaveRequest {
  starLevel: CriterionModel;
  starLevelCriterion: StarLevelCriterionModel;
}