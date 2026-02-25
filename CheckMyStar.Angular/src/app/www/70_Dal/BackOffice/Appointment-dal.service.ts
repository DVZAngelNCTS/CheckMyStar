import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../../../../Environment/environment';
import { AppointmentGetRequest } from '../../40_Requests/BackOffice/Appointment-get.request';
import { AppointmentSaveRequest } from '../../40_Requests/BackOffice/Appointment-save.request';
import { AppointmentDeleteRequest } from '../../40_Requests/BackOffice/Appointment-delete.request';
import { AppointmentResponse } from '../../50_Responses/BackOffice/Appointment.response';
import { BaseResponse } from '../../50_Responses/BaseResponse';

@Injectable({
  providedIn: 'root'
})
export class AppointmentDalService {
  private apiUrl = Environment.ApiUrl;

  constructor(private http: HttpClient) {}

  getNextIdentifier$(): Observable<AppointmentResponse> {
    return this.http.get<AppointmentResponse>(`${this.apiUrl}/Appointment/getnextidentifier`);
  }

  getAppointmentByFolder$(request: AppointmentGetRequest): Observable<AppointmentResponse> {
    let params = new HttpParams();

    Object.keys(request).forEach(key => {
      const value = (request as any)[key];
      if (value !== undefined && value !== null) {
        params = params.set(key, value);
      }
    });

    return this.http.get<AppointmentResponse>(`${this.apiUrl}/Appointment/getappointmentbyfolder`, { params });
  }

  addAppointment$(request: AppointmentSaveRequest): Observable<AppointmentResponse> {
    return this.http.post<AppointmentResponse>(`${this.apiUrl}/Appointment/addappointment`, request);
  }

  updateAppointment$(request: AppointmentSaveRequest): Observable<AppointmentResponse> {
    return this.http.put<AppointmentResponse>(`${this.apiUrl}/Appointment/updateappointment`, request);
  }

  deleteAppointment$(request: AppointmentDeleteRequest): Observable<BaseResponse> {
    let params = new HttpParams();

    Object.keys(request).forEach(key => {
      const value = (request as any)[key];
      if (value !== undefined && value !== null) {
        params = params.set(key, value);
      }
    });

    return this.http.request<BaseResponse>('delete', `${this.apiUrl}/Appointment/deleteappointment`, { params });
  }
}
