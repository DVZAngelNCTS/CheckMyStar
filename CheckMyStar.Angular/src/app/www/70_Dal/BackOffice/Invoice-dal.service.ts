import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { InvoicesResponse } from '../../50_Responses/BackOffice/Invoices.response';
import { InvoiceResponse } from '../../50_Responses/BackOffice/Invoice.response';
import { InvoiceGetRequest } from '../../40_Requests/BackOffice/Invoice-get.request';
import { InvoiceSaveRequest } from '../../40_Requests/BackOffice/Invoice-save.request';
import { InvoiceDeleteRequest } from '../../40_Requests/BackOffice/Invoice-delete.request';
import { BaseResponse } from '../../50_Responses/BaseResponse';

@Injectable({
  providedIn: 'root'
})
export class InvoiceDalService {
  private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {}

  getInvoices$(request: InvoiceGetRequest): Observable<InvoicesResponse> {
    let params = new HttpParams();

    Object.keys(request).forEach(key => {
      const value = (request as any)[key];
      if (value !== undefined && value !== null && value !== '') {
        params = params.set(key, value);
      }
    });

    return this.http.get<InvoicesResponse>(`${this.apiUrl}/Invoice/getinvoices`, { params });
  }

  getInvoicesByInspector$(request: InvoiceGetRequest): Observable<InvoicesResponse> {
    let params = new HttpParams();

    Object.keys(request).forEach(key => {
      const value = (request as any)[key];
      if (value !== undefined && value !== null && value !== '') {
        params = params.set(key, value);
      }
    });

    return this.http.get<InvoicesResponse>(`${this.apiUrl}/Invoice/getinvoicesbyinspector`, { params });
  }

  getNextIdentifier$(): Observable<InvoiceResponse> {
    return this.http.get<InvoiceResponse>(`${this.apiUrl}/Invoice/getnextidentifier`);
  }

  addInvoice$(request: InvoiceSaveRequest): Observable<BaseResponse> {
    return this.http.post<BaseResponse>(`${this.apiUrl}/Invoice/addinvoice`, request);
  }

  updateInvoice$(request: InvoiceSaveRequest): Observable<BaseResponse> {
    return this.http.put<BaseResponse>(`${this.apiUrl}/Invoice/updateinvoice`, request);
  }

  deleteInvoice$(request: InvoiceDeleteRequest): Observable<BaseResponse> {
    return this.http.delete<BaseResponse>(`${this.apiUrl}/Invoice/deleteinvoice`, { body: request });
  }
}
