import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { Environment } from '../../../../Environment/environment';
import { FoldersResponse } from '../../50_Responses/BackOffice/Folders.response';
import { FolderResponse } from '../../50_Responses/BackOffice/Folder.response';
import { FolderGetRequest } from '../../40_Requests/BackOffice/Folder-get.request';
import { FolderDeleteRequest } from '../../40_Requests/BackOffice/Folder-delete.request';
import { FolderModel } from '../../20_Models/BackOffice/Folder.model';
import { BaseResponse } from '../../50_Responses/BaseResponse';

@Injectable({
  providedIn: 'root'
})
export class FolderDalService {
  private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {}

  getNextIdentifier$(): Observable<FolderResponse> {
    return this.http.get<FolderResponse>(`${this.apiUrl}/Folder/getnextidentifier`, {});
  }

  getFolders$(request: FolderGetRequest): Observable<FoldersResponse> {
    let params = new HttpParams();

    Object.keys(request).forEach(key => {
      const value = (request as any)[key];
      if (value !== undefined && value !== null) {
        params = params.set(key, value);
      }
    });

    return this.http.get<FoldersResponse>(`${this.apiUrl}/Folder/getfolders`, { params });
  }

  getFolder$(request: FolderGetRequest): Observable<FolderResponse> {
    let params = new HttpParams();

    Object.keys(request).forEach(key => {
      const value = (request as any)[key];
      if (value !== undefined && value !== null) {
        params = params.set(key, value);
      }
    });

    return this.http.get<FolderResponse>(`${this.apiUrl}/Folder/getfolder`, { params });
  }

  getFoldersByInspector$(request: FolderGetRequest): Observable<FoldersResponse> {
    let params = new HttpParams();

    Object.keys(request).forEach(key => {
      const value = (request as any)[key];
      if (value !== undefined && value !== null) {
        params = params.set(key, value);
      }
    });

    return this.http.get<FoldersResponse>(`${this.apiUrl}/Folder/getfoldersbyinspector`, { params });
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

  deleteFolder$(request: FolderDeleteRequest) {
    let params = new HttpParams();

    Object.keys(request).forEach(key => {
      const value = (request as any)[key];
      if (value !== undefined && value !== null) {
        params = params.set(key, value);
      }
    });

    return this.http.delete<BaseResponse>(`${this.apiUrl}/Folder/deletefolder`, { params });
  }
}
