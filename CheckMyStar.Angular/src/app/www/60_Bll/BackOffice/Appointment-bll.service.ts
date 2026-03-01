import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppointmentDalService } from '../../70_Dal/BackOffice/Appointment-dal.service';
import { AppointmentModel } from '../../20_Models/BackOffice/Appointment.model';
import { AppointmentResponse } from '../../50_Responses/BackOffice/Appointment.response';
import { AppointmentSaveRequest } from '../../40_Requests/BackOffice/Appointment-save.request';
import { AppointmentDeleteRequest } from '../../40_Requests/BackOffice/Appointment-delete.request';
import { BaseResponse } from '../../50_Responses/BaseResponse';

@Injectable({
  providedIn: 'root'
})
export class AppointmentBllService {
  constructor(private appointmentDal: AppointmentDalService) {}

  getAppointmentByFolder$(folderIdentifier: number): Observable<AppointmentResponse> {
    return this.appointmentDal.getAppointmentByFolder$({ folderIdentifier });
  }

  getNextIdentifier$(): Observable<AppointmentResponse> {
    return this.appointmentDal.getNextIdentifier$();
  }

  addAppointment$(appointment: AppointmentModel, folderIdentifier: number): Observable<AppointmentResponse> {
    const request: AppointmentSaveRequest = { appointment, folderIdentifier };
    return this.appointmentDal.addAppointment$(request);
  }

  updateAppointment$(appointment: AppointmentModel, folderIdentifier: number): Observable<AppointmentResponse> {
    const request: AppointmentSaveRequest = { appointment, folderIdentifier };
    return this.appointmentDal.updateAppointment$(request);
  }

  deleteAppointment$(identifier: number, folderIdentifier: number): Observable<BaseResponse> {
    const request: AppointmentDeleteRequest = { identifier, folderIdentifier };
    return this.appointmentDal.deleteAppointment$(request);
  }
}
