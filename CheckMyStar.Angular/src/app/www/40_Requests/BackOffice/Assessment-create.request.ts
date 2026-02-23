export interface AssessmentCriterionRequest {
  criterionId: number;
  points: number;
  status: string;
  isValidated: boolean;
  comment: string;
}

export interface CreateAssessmentRequest {
  folderIdentifier: number;
  targetStarLevel: number;
  capacity: number;
  numberOfFloors: number;
  isWhiteZone: boolean;
  isDromTom: boolean;
  isHighMountain: boolean;
  isBuildingClassified: boolean;
  isStudioNoLivingRoom: boolean;
  isParkingImpossible: boolean;
  totalArea: number;
  numberOfRooms: number;
  totalRoomsArea: number;
  smallestRoomArea: number;
  isComplete: boolean;
  criteria: AssessmentCriterionRequest[];
}

export interface UpdateAssessmentRequest extends CreateAssessmentRequest {
  identifier: number;
}
