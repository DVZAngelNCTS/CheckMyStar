import { StarCriteriaDetail } from "../../20_Models/BackOffice/Criteres.model";
import { BaseResponse } from "../BaseResponse";

export interface CriteriaDetailsResponse extends BaseResponse {
    starCriterias?: StarCriteriaDetail[]
}