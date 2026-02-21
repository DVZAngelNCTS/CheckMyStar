import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { CountriesResponse } from '../../50_Responses/BackOffice/Countries.response';

@Injectable({
  providedIn: 'root'
})
export class CountryDalService {
   private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {
  }

  getCountries$(): Observable<CountriesResponse> {
    return this.http.get<CountriesResponse>(`${this.apiUrl}/Country/getcountries`, {});
  }
}