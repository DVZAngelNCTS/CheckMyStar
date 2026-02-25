import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { AddressResponse } from '../../50_Responses/BackOffice/Address.response';
import { AddressModel } from '../../20_Models/Common/Address.model';
import { GeolocationResponse } from '../../50_Responses/Common/Geolocation.response';
import { GeolocationGetRequest } from '../../40_Requests/Common/Geolocation-get.request';

@Injectable({
  providedIn: 'root'
})
export class AddressDalService {
   private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {
  }

  getNextIdentifier$(): Observable<AddressResponse> {
    return this.http.get<AddressResponse>(`${this.apiUrl}/Address/getnextidentifier`, {});
  }

  addAddress$(payload: { address: AddressModel }): Observable<AddressResponse> {
    return this.http.post<AddressResponse>(`${this.apiUrl}/Address/addaddress`, payload);
  }

  updateAddress$(payload: { address: AddressModel }): Observable<AddressResponse> {
    return this.http.put<AddressResponse>(`${this.apiUrl}/Address/updateaddress`, payload);
  }

  searchAddress$(request: GeolocationGetRequest): Observable<GeolocationResponse> {
    if (!request.address || request.address.length < 3) {
      return new Observable<GeolocationResponse>(obs => {
        obs.next({
          isSuccess: true,
          message: '',
          addresses: []
        });
        obs.complete();
      });
    }

    let params = new HttpParams();

    Object.keys(request).forEach(key => {
      const value = (request as any)[key];
      if (value !== undefined && value !== null) {
        params = params.set(key, value);
      }
    });

    return this.http.get<GeolocationResponse>(`${this.apiUrl}/Address/searchaddress`, { params })
  }
}