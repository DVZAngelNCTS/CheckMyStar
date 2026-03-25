import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { QuotesResponse } from '../../50_Responses/BackOffice/Quotes.response';
import { QuoteResponse } from '../../50_Responses/BackOffice/Quote.response';
import { QuoteGetRequest } from '../../40_Requests/BackOffice/Quote-get.request';
import { QuoteSaveRequest } from '../../40_Requests/BackOffice/Quote-save.request';
import { QuoteDeleteRequest } from '../../40_Requests/BackOffice/Quote-delete.request';
import { BaseResponse } from '../../50_Responses/BaseResponse';

@Injectable({
  providedIn: 'root'
})
export class QuoteDalService {
  private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {}

  getQuotes$(request: QuoteGetRequest): Observable<QuotesResponse> {
    let params = new HttpParams();

    Object.keys(request).forEach(key => {
      const value = (request as any)[key];
      if (value !== undefined && value !== null) {
        params = params.set(key, value);
      }
    });

    return this.http.get<QuotesResponse>(`${this.apiUrl}/Quote/getquotes`, { params });
  }

  getNextIdentifier$(): Observable<QuoteResponse> {
    return this.http.get<QuoteResponse>(`${this.apiUrl}/Quote/getnextidentifier`);
  }

  addQuote$(request: QuoteSaveRequest): Observable<BaseResponse> {
    return this.http.post<BaseResponse>(`${this.apiUrl}/Quote/addquote`, request);
  }

  updateQuote$(request: QuoteSaveRequest): Observable<BaseResponse> {
    return this.http.put<BaseResponse>(`${this.apiUrl}/Quote/updatequote`, request);
  }

  deleteQuote$(request: QuoteDeleteRequest): Observable<BaseResponse> {
    return this.http.delete<BaseResponse>(`${this.apiUrl}/Quote/deletequote`, { body: request });
  }

  enabledQuote$(request: QuoteSaveRequest): Observable<BaseResponse> {
    return this.http.put<BaseResponse>(`${this.apiUrl}/Quote/enabledquote`, request);
  }

  generatePdf$(identifier: number): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/Quote/generatepdf/${identifier}`, { responseType: 'blob' });
  }
}
