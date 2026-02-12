export interface StarStatus {
  code: string;
  label: string;
  count: number;
}

export interface StarCriteria {
  rating: number;
  label: string;
  description: string;
  lastUpdate: string;
  statuses: StarStatus[];
}

export interface StarCriterionDetail {
  criterionId: number;
  description: string;
  basePoints: number;
  typeCode: string;
  typeLabel: string;
}

export interface StarCriteriaDetail {
  rating: number;
  starLabel: string;
  criteria: StarCriterionDetail[];
}

export interface CreateCriterionRequest {
  description: string;
  basePoints: number;
  starLevels: StarLevelAssignment[];
}

export interface StarLevelAssignment {
  starLevelId: number;
  typeCode: string;
}