import { UserModel } from '../Common/User.model';
import { AddressModel } from '../Common/Address.model';
import { SocietyModel } from './Society.model';

export interface InvoiceLineModel {
    identifier: number,
    invoiceIdentifier: number,
    description: string,
    quantity: number,
    unit: string,
    unitPriceHT: number,
    vatRate: number,
    createdDate?: string,
    updatedDate?: string,
}

export interface InvoiceModel {
    identifier: number,
    invoiceNumber?: string,
    quoteIdentifier?: number,
    clientUserIdentifier?: number,
    clientUser?: UserModel | null,
    clientAddressIdentifier?: number,
    clientAddress?: AddressModel | null,
    companySocietyIdentifier?: number,
    companySociety?: SocietyModel | null,
    companyAddressIdentifier?: number,
    companyAddress?: AddressModel | null,
    invoiceDate?: string,
    totalAmountHT?: number,
    totalVATAmount?: number,
    totalAmountTTC?: number,
    paymentStatusIdentifier?: number,
    createdDate?: string,
    dueDate?: string,
    updatedDate?: string,
    invoiceLines?: InvoiceLineModel[],
    isActive?: boolean,

    // Legacy fields kept for compatibility with any remaining usages.
    number?: string,
    amount?: number,
    isPaid?: boolean,
}