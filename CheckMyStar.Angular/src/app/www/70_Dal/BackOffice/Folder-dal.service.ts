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
import { FolderSaveRequest } from '../../40_Requests/BackOffice/Folder-save.request';

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

  addFolder$(request: FolderSaveRequest) {    
    return this.http.post<BaseResponse>(`${this.apiUrl}/Folder/addFolder`, request);
  }

  updateFolder$(request: FolderSaveRequest) {
    return this.http.put<BaseResponse>(`${this.apiUrl}/Folder/updateFolder`, request);
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
