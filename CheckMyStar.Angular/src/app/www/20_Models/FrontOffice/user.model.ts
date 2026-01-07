import { EnumCivility } from '../../10_Common/Enumerations/EnumCivility';
import { EnumRole } from '../../10_Common/Enumerations/EnumRole';
import { AddressModel } from './address.model';


export interface UserModel {
    identifier: number;
    civility: EnumCivility;
    lastName: string;
    firstName: string;
    society: string;
    email: string;
    phone: string;
    password: string;
    role: EnumRole;
    address: AddressModel;
} 