import { StarStatusModel } from './StarStatus.model';

export interface StarCriteriaModel {
  rating: number;
  label: string;
  description: string;
  lastUpdate: string;
  statuses: StarStatusModel[];
}
