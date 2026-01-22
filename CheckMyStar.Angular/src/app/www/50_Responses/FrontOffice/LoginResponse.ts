import { LoginModel } from "../../20_Models/FrontOffice/Login.model";
import { BaseResponse } from "../BaseResponse";

export interface LoginResponse extends BaseResponse {
    isValid: boolean;
    login: LoginModel;
}