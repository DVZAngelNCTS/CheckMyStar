import { Injectable } from '@angular/core';
import { ActivityDalService } from '../../70_Dal/BackOffice/Activity-dal.service';
import { ActivityGetRequest } from '../../40_Requests/BackOffice/Activity-get.request';
import { ActivitiesGetRequest } from '../../40_Requests/BackOffice/Activities-get.request';

@Injectable({
  providedIn: 'root'
})
export class ActivityBllService {
  constructor(private activityDal: ActivityDalService) {
  }

  getActivities$(numberDays: number) {
    const request = { numberDays: numberDays } as ActivityGetRequest;

    return this.activityDal.getActivities$(request);
  }

  getDetailActivities$(lastName?: string, firstName?: string, description?: string, createdDate?: Date, isSuccess?: boolean) {
    const request = { lastName, firstName, description, createdDate, isSuccess } as ActivitiesGetRequest;

    return this.activityDal.getDetailActivities$(request);
  }
}