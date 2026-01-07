import { CountryModel } from './country.model';

export interface AddressModel {
    identifier: number;
    number: number;
    addressLine: string;
    city: string;
    zipCode: string;
    region: string;
    country: CountryModel;
}