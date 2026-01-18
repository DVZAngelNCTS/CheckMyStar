import { UserModel } from './User.model';

export interface LoginModel {
    token: string;
    user: UserModel;
}