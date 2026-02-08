import { DashboardModel } from "../../20_Models/BackOffice/Dashboard.model";
import { BaseResponse } from "../BaseResponse";

export interface DashboardResponse extends BaseResponse {
    dashboard?: DashboardModel
}