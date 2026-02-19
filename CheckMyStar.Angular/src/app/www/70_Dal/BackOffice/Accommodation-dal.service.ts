import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { Environment } from '../../../../Environment/environment';
import { AccommodationModel } from '../../20_Models/BackOffice/Folder.model';

@Injectable({
  providedIn: 'root'
})
export class AccommodationDalService {
  private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {}

  getNextIdentifier$(): Observable<number> {
    const url = `${this.apiUrl}/Accommodation/getnextidentifier`;
    return this.http.post<any>(url, {}).pipe(
      switchMap(response => {
        console.log('Next accommodation identifier response:', response);
        
        // Extract the actual numeric identifier from the response
        let identifier: number;
        
        if (response.accommodation && response.accommodation.identifier) {
          identifier = response.accommodation.identifier;
        } else if (response.identifier) {
          identifier = response.identifier;
        } else if (response.nextIdentifier) {
          identifier = response.nextIdentifier;
        } else if (typeof response === 'number') {
          identifier = response;
        } else {
          console.error('Unable to extract identifier from response:', response);
          return throwError(() => new Error('Format de réponse invalide pour l\'identifiant'));
        }
        
        console.log('Extracted accommodation identifier:', identifier);
        return of(identifier);
      })
    );
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
}
