import { UserModel } from '../Common/User.model';
import { AddressModel } from '../Common/Address.model';
import { SocietyModel } from './Society.model';

export interface QuoteLineModel {
    identifier: number,
    quoteIdentifier: number,
    description: string,
    quantity: number,
    unit: string,
    unitPriceHT: number,
    vatRate: number,
    createdDate?: string,
    updatedDate?: string,
}

export interface QuoteModel {
    identifier: number,
    reference?: string,
    createdDate?: string,
    clientName?: string,
    totalAmountTTC?: number,
    validityDate?: string,
    quoteStatus?: string,
    clientUserIdentifier?: number,
    clientUser?: UserModel,
    clientAddressIdentifier?: number,
    clientAddress?: AddressModel,
    inspectorIdentifier?: number,
    companySocietyIdentifier?: number,
    companySociety?: SocietyModel,
    companyAddressIdentifier?: number,
    companyAddress?: AddressModel,
    companyLogoPath?: string,
    companyEmail?: string,
    companyPhone?: string,
    companySiretCode?: string,
    companyVatNumber?: string,
    companyLegalInformation?: string,
    totalAmountHT?: number,
    quoteStatusIdentifier?: number,
    executionDate?: string,
    updatedDate?: string,
    isEditable?: boolean,
    isActive?: boolean,
    quoteLines?: QuoteLineModel[],
}