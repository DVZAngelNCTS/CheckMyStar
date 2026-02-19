export interface EvaluationFormData {
  targetStar: number | null;
  maxCapacity: number | null;
  floors: number | null;
  isZoneBlanche: boolean;
  isDromTom: boolean;
  isHauteMontagne: boolean;
  isBatimentClasse: boolean;
  isStudioSansSejouur: boolean;
  isStationnementImpossible: boolean;
  totalArea: number | null;
  roomCount: number | null;
  totalRoomsArea: number | null;
  smallestRoomArea: number | null;
}

export interface CriterionEvaluation {
  criterionId: number;
  description: string;
  basePoints: number;
  typeCode: string;
  typeLabel: string;
  validated: boolean;
  comment: string;
}

export interface EvaluationResult {
  accepted: boolean;
  mandatoryOk: boolean;
  optionalOk: boolean;
  oncOk: boolean;
  earnedMandatoryPoints: number;
  mandatoryThreshold: number;
  totalMandatoryPoints: number;
  earnedOptionalPoints: number;
  requiredOptional: number;
  totalOptionalPoints: number;
  minOptionalRequired: number;
  bonus: number;
  missingMandatory: number;
  oncCount: number;
  oncFailedCount: number;
}