import { CountryModel } from './country.model';

export interface AddressModel {
    Identifier: number;
    Number: number;
    AddressLine: string;
    City: string;
    ZipCode: string;
    Region: string;
    Country: CountryModel;
}