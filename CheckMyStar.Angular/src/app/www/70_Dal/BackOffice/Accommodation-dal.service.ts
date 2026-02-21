import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { Environment } from '../../../../Environment/environment';
import { AccommodationModel } from '../../20_Models/BackOffice/Folder.model';
import { AccommodationResponse } from '../../50_Responses/BackOffice/Accommodation.response';

@Injectable({
  providedIn: 'root'
})
export class AccommodationDalService {
  private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {}

  getNextIdentifier$(): Observable<AccommodationResponse> {
    return this.http.get<AccommodationResponse>(`${this.apiUrl}/Accommodation/getnextidentifier`, {});
  }

  createAccommodation$(accommodation: AccommodationModel): Observable<AccommodationModel> {
    const url = `${this.apiUrl}/Accommodation/createaccommodation`;
    return this.http.post<any>(url, { accommodation }).pipe(
      switchMap(response => {
        console.log('Raw API response for accommodation:', response);

        if (!response.isSuccess) {
          return throwError(() => new Error(response.message || 'Erreur lors de la création de l\'hébergement'));
        }

        // API only returns {isSuccess, message}, use the accommodation we sent (already has identifier from getnextidentifier)
        return of(accommodation);
      })
    );
  }

  updateAccommodation$(accommodation: AccommodationModel): Observable<AccommodationModel> {
    const url = `${this.apiUrl}/Accommodation/updateaccommodation`;
    return this.http.put<any>(url, { accommodation }).pipe(
      switchMap(response => {
        if (!response.isSuccess) {
          return throwError(() => new Error(response.message || 'Erreur lors de la mise à jour de l\'hébergement'));
        }
        return of(accommodation);
      })
    );
  }
}
