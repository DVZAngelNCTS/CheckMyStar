import { UserEvolutionModel } from "../../20_Models/BackOffice/UserEvolution.model";
import { BaseResponse } from "../BaseResponse";

export interface UserEvolutionsResponse extends BaseResponse {
    evolutions?: UserEvolutionModel[]
}