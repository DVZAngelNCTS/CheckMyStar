import { UserModel } from "../../20_Models/Common/User.model";
import { BaseResponse } from "../BaseResponse";

export interface UsersResponse extends BaseResponse {
    users?: UserModel[]
}