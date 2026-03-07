import { AddressModel } from "../../20_Models/Common/Address.model";
import { BaseResponse } from "../BaseResponse";

export interface AddressesResponse extends BaseResponse {
    addresses?: AddressModel[]
}