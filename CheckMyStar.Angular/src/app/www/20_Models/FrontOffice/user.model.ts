import { CivilityModel } from './civility.model';
import { RoleModel } from './role.model';
import { AddressModel } from './address.model';

export interface UserModel {
    Identifier: number;
    Civility: CivilityModel;
    LastName: string;
    FirstName: string;
    Society: string;
    Email: string;
    Phone: string;
    Password: string;
    Role: RoleModel;
    Address: AddressModel;
} 