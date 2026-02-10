import { UserModel } from '../Common/User.model';

export interface LoginModel {
    token: string;
    refreshToken: string;
    user: UserModel;
}
