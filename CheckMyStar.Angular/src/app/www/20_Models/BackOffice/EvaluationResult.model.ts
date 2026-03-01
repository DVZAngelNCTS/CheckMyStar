export interface EvaluationResultModel {
  identifier: number;
  assessmentIdentifier: number;
  isAccepted: boolean;
  mandatoryPointsEarned: number;
  mandatoryThreshold: number;
  optionalPointsEarned: number;
  optionalRequired: number;
  onceFailedCount: number;
  createdDate: Date;
  updatedDate: Date;
}
