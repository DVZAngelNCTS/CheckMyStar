import { AccommodationModel } from "../../20_Models/BackOffice/Accommodation.model";
import { BaseResponse } from "../BaseResponse";

export interface AccommodationResponse extends BaseResponse {
    accommodation?: AccommodationModel
}