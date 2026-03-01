export interface AssessmentCriterionModel {
  assessmentIdentifier: number;
  criterionId: number;
  criterionDescription: string;
  basePoints: number;
  points: number;
  status: string;
  isValidated: boolean;
  comment: string;
}  
