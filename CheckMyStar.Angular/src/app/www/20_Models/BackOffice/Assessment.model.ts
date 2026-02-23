import { AssessmentCriterionModel } from "./AssessmentCriterion.model";

export interface AssessmentModel {
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
  criteria: AssessmentCriterionModel[];
}