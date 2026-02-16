import { EnumCivility } from '../../10_Common/Enumerations/EnumCivility';
import { EnumRole } from '../../10_Common/Enumerations/EnumRole';
import { AddressModel } from './Address.model';


export interface UserModel {
    identifier: number;
    civility: EnumCivility;
    lastName: string;
    firstName: string;
    societyIdentifier?: number | null;
    email: string;
    phone: string;
    password: string;
    role: EnumRole;
    address: AddressModel;
    isActive: boolean;
} 