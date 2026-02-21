import { StarCriteriaDetailModel } from "../../20_Models/BackOffice/StarCriteriaDetail.model";
import { BaseResponse } from "../BaseResponse";

export interface CriteriaDetailsResponse extends BaseResponse {
    starCriterias?: StarCriteriaDetailModel[]
}