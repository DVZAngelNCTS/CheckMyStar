import { CountryModel } from "../../20_Models/Common/Country.model";
import { BaseResponse } from "../BaseResponse";

export interface CountriesResponse extends BaseResponse {
    countries?: CountryModel[]
}