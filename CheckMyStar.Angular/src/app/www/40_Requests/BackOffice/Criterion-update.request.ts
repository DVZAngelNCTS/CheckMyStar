import { StarCriterionModel } from '../../20_Models/BackOffice/Criterion.model';
import { StarLevelModel } from '../../20_Models/BackOffice/StarLevel.model';
import { StarLevelCriterionModel } from '../../20_Models/BackOffice/StarLevelCriterion.model';

export interface CriterionUpdateRequest {
  criterion : StarCriterionModel;
  starLevel: StarLevelModel;
  starLevelCriterion: StarLevelCriterionModel;
}