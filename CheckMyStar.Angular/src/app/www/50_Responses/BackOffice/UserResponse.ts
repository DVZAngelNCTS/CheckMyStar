import { UserModel } from "../../20_Models/FrontOffice/User.model";
import { BaseResponse } from "../BaseResponse";

export interface UserResponse extends BaseResponse {
    isValid: boolean,
    user?: UserModel
}