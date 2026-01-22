import { RoleModel } from "../../20_Models/BackOffice/Role.model";
import { BaseResponse } from "../BaseResponse";

export interface RoleResponse extends BaseResponse {
    role?: RoleModel
}