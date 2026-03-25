import { AddressModel } from "../Common/Address.model";

export interface SocietyModel {
    identifier: number,
    name: string,
    email: string,
    phone: string,
    logoPath?: string,
    siretCode?: string,
    vatNumber?: string,
    legalInformation?: string,
    address: AddressModel,
    isActive: boolean
}