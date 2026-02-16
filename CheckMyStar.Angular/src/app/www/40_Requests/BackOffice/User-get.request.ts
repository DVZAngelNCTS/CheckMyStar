export interface UserGetRequest {
    lastName: string;
    firstName: string;
    societyIdentifier: Number;
    email: string;
    phone: string;
    address: string;
    role?: number;
}