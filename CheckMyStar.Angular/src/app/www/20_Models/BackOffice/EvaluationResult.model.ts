export interface EvaluationResultModel {
  identifier: number;
  assesmentIdentifier: number;
  isAccepted: boolean;
  mandatoryPointsEarned: number;
  mandatoryThreshold: number;
  optionalPointsEarned: number;
  optionalRequired: number;
  oncFailedCount: number;
  createdDate: Date;
  updatedDate: Date;
}
