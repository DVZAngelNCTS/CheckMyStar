
import { StarCriterionDetailModel } from './StarCriterionsDetail.model';

export interface StarCriteriaDetailModel {
  rating: number;
  starLabel: string;
  criteria: StarCriterionDetailModel[];
}
