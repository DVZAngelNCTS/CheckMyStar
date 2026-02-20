import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { Environment } from '../../../../Environment/environment';
import { FoldersResponse } from '../../50_Responses/BackOffice/Folders.response';
import { FolderGetRequest } from '../../40_Requests/BackOffice/Folder-get.request';
import { FolderModel } from '../../20_Models/BackOffice/Folder.model';
import { BaseResponse } from '../../50_Responses/BaseResponse';

@Injectable({
  providedIn: 'root'
})
export class FolderDalService {
  private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {}

  getNextIdentifier$(): Observable<number> {
    const url = `${this.apiUrl}/Folder/getnextidentifier`;
    return this.http.post<any>(url, {}).pipe(
      switchMap(response => {
        console.log('Next folder identifier response:', response);
        
        // Extract the actual numeric identifier from the response
        let identifier: number;
        
        if (response.folder && response.folder.identifier) {
          identifier = response.folder.identifier;
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
        
        console.log('Extracted folder identifier:', identifier);
        return of(identifier);
      })
    );
  }

  getFolders$(request: FolderGetRequest): Observable<FoldersResponse> {
    let params = new HttpParams();

    if (request.accommodationName) {
      params = params.set('accommodationName', request.accommodationName);
    }
    if (request.ownerLastName) {
      params = params.set('ownerLastName', request.ownerLastName);
    }
    if (request.inspectorLastName) {
      params = params.set('inspectorLastName', request.inspectorLastName);
    }
    if (request.folderStatus) {
      params = params.set('folderStatus', request.folderStatus.toString());
    }

    const url = `${this.apiUrl}/Folder/getfolders`;
    return this.http.get<FoldersResponse>(url, { params });
  }

  createFolder$(folder: FolderModel): Observable<FolderModel> {
    const url = `${this.apiUrl}/Folder/createfolder`;
    console.log('Creating folder with data:', folder);
    return this.http.post<any>(url, { folder }).pipe(
      switchMap(response => {
        console.log('Raw API response for folder:', response);

        if (!response.isSuccess) {
          return throwError(() => new Error(response.message || 'Erreur lors de la création du dossier'));
        }

        // API only returns {isSuccess, message}, use the folder we sent
        return of(folder);
      })
    );
  }

  updateFolder$(folder: FolderModel): Observable<FolderModel> {
    const url = `${this.apiUrl}/Folder/updatefolder`;
    return this.http.put<any>(url, { folder }).pipe(
      switchMap(response => {
        if (!response.isSuccess) {
          return throwError(() => new Error(response.message || 'Erreur lors de la mise à jour du dossier'));
        }
        return of(folder);
      })
    );
  }

  deleteFolder$(folderIdentifier: number): Observable<BaseResponse> {
    const url = `${this.apiUrl}/Folder/deletefolder/${folderIdentifier}`;
    return this.http.delete<BaseResponse>(url);
  }
}
