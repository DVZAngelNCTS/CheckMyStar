import { ActivityModel } from "../../20_Models/BackOffice/Activity.model";
import { BaseResponse } from "../BaseResponse";

export interface ActivityResponse extends BaseResponse {
    activities?: ActivityModel[]
}