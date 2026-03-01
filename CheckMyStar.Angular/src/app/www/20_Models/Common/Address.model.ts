import { CountryModel } from './Country.model';

export interface AddressModel {
    identifier: number,
    number?: string,
    addressLine?: string,
    city?: string,
    zipCode?: string,
    region?: string,
    country?: CountryModel
}