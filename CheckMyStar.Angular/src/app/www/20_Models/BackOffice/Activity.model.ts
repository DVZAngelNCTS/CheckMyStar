import { UserModel } from "../Common/User.model";

export interface ActivityModel {
    identifier: number,
    description: string,
    date: Date,
    user: UserModel,
    isSuccess: boolean
}