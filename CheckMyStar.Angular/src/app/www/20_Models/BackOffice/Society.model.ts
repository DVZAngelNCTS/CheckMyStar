import { AddressModel } from "../Common/Address.model";

export interface SocietyModel {
    identifier: number,
    name: string,
    email: string,
    phone: string,
    addressIdentifier: number,
    isActive: boolean
}