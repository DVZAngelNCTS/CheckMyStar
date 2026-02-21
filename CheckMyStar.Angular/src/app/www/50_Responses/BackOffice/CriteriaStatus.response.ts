import { StarCriteriaModel } from "../../20_Models/BackOffice/Criteres.model";
import { BaseResponse } from "../BaseResponse";

export interface CriteriaStatusResponse extends BaseResponse {
    starCriterias?: StarCriteriaModel[]
}