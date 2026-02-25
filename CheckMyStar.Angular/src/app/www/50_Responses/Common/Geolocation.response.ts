import { GeolocationAddressModel } from "../../20_Models/Common/GeolocationAddress.model";
import { BaseResponse } from "../BaseResponse";

export interface GeolocationResponse extends BaseResponse {
    addresses?: GeolocationAddressModel[]
}