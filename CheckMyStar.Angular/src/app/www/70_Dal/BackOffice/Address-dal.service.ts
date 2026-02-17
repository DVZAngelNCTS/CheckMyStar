import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { AddressResponse } from '../../50_Responses/BackOffice/Address.response';
import { AddressModel } from '../../20_Models/Common/Address.model';

@Injectable({
  providedIn: 'root'
})
export class AddressDalService {
   private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {
  }

  getNextIdentifier$(): Observable<AddressResponse> {
    return this.http.post<AddressResponse>(`${this.apiUrl}/Address/getnextidentifier`, {});
  }

  addAddress$(payload: { address: AddressModel }): Observable<AddressResponse> {
    return this.http.post<AddressResponse>(`${this.apiUrl}/Address/addaddress`, payload);
  }
}