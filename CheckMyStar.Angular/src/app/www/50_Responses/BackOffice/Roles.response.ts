import { RoleModel } from "../../20_Models/BackOffice/Role.model";
import { BaseResponse } from "../BaseResponse";

export interface RolesResponse extends BaseResponse {
    roles?: RoleModel[]
}