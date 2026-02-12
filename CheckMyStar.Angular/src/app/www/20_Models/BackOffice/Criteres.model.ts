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
  starCriterion: {
    description: string;
    basePoints: number;
  };
  starLevelCriterion: {
    starLevelId: number;
    typeCode: string;
  };
}

export interface StarLevelAssignment {
  starLevelId: number;
  typeCode: string;
}

export interface UpdateCriterionRequest {
  criterionId: number;
  description: string;
  basePoints: number;
  typeCode: string;
  starLevelId: number;
}